using System;
using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {

    [TestClass]
    public class RuleFactoryTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(RuleFactory);

        [DataTestMethod]
        [DataRow(RuleKind.ActivityRule, typeof(ActivityRule))]
        [DataRow(RuleKind.Rule, typeof(Rule))]
        [DataRow(RuleKind.Unspecified, typeof(Rule))]
        public void CreateTest(RuleKind kind, Type t) {
            var d = GetRandom.ObjectOf<RuleData>();
            d.RuleKind = kind;
            var o = RuleFactory.Create(d);
            Assert.IsInstanceOfType(o, t);
            if (kind == RuleKind.Unspecified)
                Assert.IsTrue(o.IsUnspecified);
            else allPropertiesAreEqual(d, o.Data);

        }

    }

}
