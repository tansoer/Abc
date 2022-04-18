using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {

    [TestClass]
    public class UnitTests : AbstractTests<Unit, BaseMetric<UnitData>> {

        private class testClass : Unit {
            internal string result;
            public testClass(UnitData d) : base(d) { }
            public override double ToBase(in double amount) => amount;
            public override double FromBase(in double amount) => amount;
            protected override Unit multiply(DerivedUnit m, string n = null, string c = null, string d = null) {
                result += "Derived";
                return new DerivedUnit(new UnitData());
            }
            protected override Unit multiply(FactoredUnit m, string n = null, string c = null, string d = null) {
                result += "Factored";
                return new DerivedUnit(new UnitData());
            }
            protected override Unit multiply(FunctionedUnit m, string n = null, string c = null, string d = null) {
                result += "Functioned";
                return new DerivedUnit(new UnitData());
            }
            protected override Unit toPower(in double power, string n = null, string c = null, string d = null) {
                result += ("Power" + power);
                return new DerivedUnit(new UnitData());
            }
            protected override string formula(bool isLong = false) => isLong.ToString();
        }
        protected override Unit createObject() => 
            new testClass(GetRandom.ObjectOf<UnitData>());

        [TestMethod] public void MeasureIdTest() => isReadOnly(obj.Data.MeasureId);

        [TestMethod] public void MeasureTest() {
            var m = GetRandom.ObjectOf<MeasureData>();
            m.MeasureType = MeasureType.Base;
            m.Id = obj.MeasureId;
            GetRepo.Instance<IMeasuresRepo>().Add(new BaseMeasure(m));
            allPropertiesAreEqual(m, obj.Measure.Data);
        }
        [TestMethod] public void DivideTest() {
            testDivide(new FunctionedUnit(new UnitData()), "Derived");
            testDivide(new FactoredUnit(new UnitData()), "Derived");
            testDivide(new DerivedUnit(new UnitData()), "Derived");
        }

        [TestMethod] public void SqrtTest() => validate(obj.Sqrt(), "Power0.5");
        private void validate(Unit u, string s) {
            Assert.IsNotNull(u);
            var o = obj as testClass;
            var toCompare = o?.result;
            if (o != null && toCompare != s && toCompare.Contains(',')) toCompare = toCompare.Replace(',', '.');
            Assert.AreEqual(s, toCompare);
            if (o is null) return;
            o.result = "";
        }
        [TestMethod] public void MultiplyTest() {
            testMultiply(new FunctionedUnit(new UnitData()), "Functioned");
            testMultiply(new FactoredUnit(new UnitData()), "Factored");
            testMultiply(new DerivedUnit(new UnitData()), "Derived");
        }
        [TestMethod]
        public void InverseTest() {
            var x = obj.Inverse();
            Assert.IsNotNull(x);
            Assert.AreEqual("Power" + -1, (obj as testClass)?.result);
        }
        [TestMethod] public void PowerTest() {
            var i = GetRandom.Int32();
            var x = obj.Power(i);
            Assert.IsNotNull(x);
            var toCompare = "Power" + i;
            if ("Power" + i != (obj as testClass)?.result && toCompare.Contains(','))
                toCompare = toCompare.Replace(',', '.');
            Assert.AreEqual(toCompare, (obj as testClass)?.result);
        }
        [TestMethod] public void FormulaTest() {
            Assert.AreEqual("True", obj.Formula(true));
            Assert.AreEqual("False", obj.Formula());
        }
        [TestMethod] public void ToBaseTest() => isAbstractMethod(nameof(obj.ToBase));

        [TestMethod] public void FromBaseTest() => isAbstractMethod(nameof(obj.FromBase));
        private void testMultiply(Unit u, string r) => validate(obj.Multiply(u), r);
        private void testDivide(Unit u, string r) => validate(obj.Divide(u), r);
    }
}