using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Quantities {

    [TestClass]
    public class MeasureTests : AbstractTests<Measure, BaseMetric<MeasureData>> {
        private class testClass :Measure {
            internal string result;
            public testClass(MeasureData d) : base(d) { }
            protected override string formula(bool isLong = false) => isLong.ToString();
            protected override Measure multiply(DerivedMeasure m, string n = null, string c = null, string d = null) {
                result += "Derived";
                return new DerivedMeasure(new MeasureData());
            }
            protected override Measure multiply(BaseMeasure m, string n = null, string c = null, string d = null) {
                result += "Base";
                return new DerivedMeasure(new MeasureData());
            }
            protected override Measure power(in double power, string n = null, string c = null, string d = null) {
                result += "Power" + power;
                return new DerivedMeasure(new MeasureData());
            }
        }
        protected override Measure createObject() =>
            new testClass(GetRandom.ObjectOf<MeasureData>());
        [TestMethod] public void DivideTest() {
            testDivide(new BaseMeasure(new MeasureData()), "Derived");
            testDivide(new DerivedMeasure(new MeasureData()), "Derived");
        }
        [TestMethod] public void FormulaTest() {
            Assert.AreEqual("True", obj.Formula(true));
            Assert.AreEqual("False", obj.Formula());
        }
        [TestMethod] public void InverseTest() {
            var x = obj.Inverse();
            Assert.IsNotNull(x);
            Assert.AreEqual("Power" + -1, (obj as testClass)?.result);
        }
        [TestMethod] public void MultiplyTest() {
            testMultiply(new BaseMeasure(new MeasureData()), "Base");
            testMultiply(new DerivedMeasure(new MeasureData()), "Derived");
        }
        [TestMethod] public void PowerTest() {
            var i = GetRandom.Int32();
            var x = obj.Power(i);
            Assert.IsNotNull(x);
            Assert.AreEqual("Power" + i, (obj as testClass)?.result);
        }
        [TestMethod] public void SqrtTest() => validate(obj.Sqrt(), "Power0.5");
        [TestMethod] public async Task UnitsTest() =>
            await testListAsync<Unit, UnitData, IUnitsRepo>(
                d => d.MeasureId = obj.Id,
                UnitFactory.Create);
        private void testDivide(Measure m, string r) => validate(obj.Divide(m), r);
        private void testMultiply(Measure m, string r)  => validate(obj.Multiply(m), r);
        private void validate(Measure u, string s) {
            Assert.IsNotNull(u);
            var o = obj as testClass;
            if (o != null && o.result.Contains(',') && s.Contains('.')) s = s.Replace('.', ',');
            Assert.AreEqual(s, o?.result);

            if (o is null) return;
            o.result = "";
        }
    }
}