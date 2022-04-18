using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {

    [TestClass] public class RuleElementTests : 
        AbstractTests<RuleElement, Entity<RuleElementData>> {
        private class testClass : RuleElement {
            public testClass(RuleElementData d) : base(d) { }
        }
        protected override RuleElement createObject() => 
            new testClass(GetRandom.ObjectOf<RuleElementData>());
        [TestMethod] public void TypeTest() => isReadOnly(obj.Data.RuleElementType);
        [TestMethod] public void IndexTest() => isReadOnly(obj.Data.Index);
        [TestMethod] public void RuleIdTest() => isReadOnly(obj.Data.RuleId);
        [TestMethod] public void RuleContextIdTest() => isReadOnly(obj.Data.RuleContextId);
        
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void IsRuleElementTest(bool b) {
            var d = GetRandom.ObjectOf<RuleElementData>();
            if (b) d.RuleContextId = null;
            else d.RuleId = null;
            obj = new testClass(d);
            isReadOnly(b);
        }
        
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void IsContextElementTest(bool b) {
            var d = GetRandom.ObjectOf<RuleElementData>();
            if (b) d.RuleId = null;
            else d.RuleContextId = null;
            obj = new testClass(d);
            isReadOnly(b);
        }
        
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void MasterIdTest(bool b) {
            var d = GetRandom.ObjectOf<RuleElementData>();
            if (b) d.RuleContextId = null;
            else d.RuleId = null;
            obj = new testClass(d);
            isReadOnly(b ? obj.Data.RuleId : obj.Data.RuleContextId);
        }
    }
}
