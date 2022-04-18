using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Rules;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Facade.Rules {

    [TestClass]
    public class RuleViewFactoryTests :
        ViewFactoryTests<RuleViewFactory, RuleData, BaseRule, RuleView> {

        private RuleKind ruleKind;
        private Type ruleType;
        protected override RuleView createView() {
            var v = base.createView();
            v.RuleKind = ruleKind;
            return v;
        }
        protected override RuleData createData() {
            var d = base.createData();
            d.RuleKind = ruleKind;
            return d;
        }
        protected override BaseRule dataToObject(RuleData d) {
            var o = base.dataToObject(d);
            Assert.IsInstanceOfType(o, ruleType);
            return o;
        }

        [DataRow(RuleKind.Rule)]
        [DataRow(RuleKind.ActivityRule)]
        [TestMethod]
        public void CreateTest(RuleKind k) {
            setKind(k);
            base.CreateTest();
        }

        private void setKind(RuleKind k) {
            ruleKind = k;
            ruleType = (k == RuleKind.Rule) ? typeof(Rule) : typeof(ActivityRule);
        }

        [DataRow(RuleKind.Rule)]
        [DataRow(RuleKind.ActivityRule)]
        [TestMethod]
        public void CreateObjectTest(RuleKind k) {
            setKind(k);
            base.CreateObjectTest();
        }

        public override void CreateTest() {}

        public override void CreateObjectTest() {}

    }

}

