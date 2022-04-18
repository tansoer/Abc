using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {

    [TestClass]
    public class BaseTermTests :AbstractTests<BaseTerm, BaseEntity<CommonTermData>> {
        private class testClass :BaseTerm {
            public testClass() : this(null) { }
            public testClass(CommonTermData d) : base(d) { }
        }
        protected override BaseTerm createObject() => new testClass();
        [TestMethod] public void PowerTest() => isReadOnly(0.0);
        [TestMethod] public void CanSetPowerInConstructorTest() {
            var d = GetRandom.ObjectOf<CommonTermData>();
            obj = new testClass(d);
            Assert.AreEqual(d.Power, obj.Power);
        }
    }
}
