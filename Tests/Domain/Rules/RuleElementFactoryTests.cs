using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {

    [TestClass]
    public class RuleElementFactoryTests : TestsBase {

        private RuleElementData d;

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(RuleElementFactory);
            d = GetRandom.ObjectOf<RuleElementData>();
        }
        [TestMethod]
        public void CreateTest() {
            var o = RuleElementFactory.Create((RuleElementData) null);
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(UnspecifiedVariable));
            Assert.IsTrue(o.IsUnspecified);
        }
        [TestMethod]
        public void CreateOperatorTest() {
            d.RuleElementType = RuleElementType.Operator;
            var o = RuleElementFactory.Create(d);
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(Operator));
            allPropertiesAreEqual(d, o.Data);
        }
        [TestMethod]
        public void CreateOperandTest() {
            d.RuleElementType = RuleElementType.Operand;
            var o = RuleElementFactory.Create(d);
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(Operand));
            allPropertiesAreEqual(d, o.Data);
        }
        [TestMethod]
        public void CreateVariableTest() {
            var o = RuleElementFactory.Create(d);
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(RuleElement));
            allPropertiesAreEqual(d, o.Data);
        }

    }

}
