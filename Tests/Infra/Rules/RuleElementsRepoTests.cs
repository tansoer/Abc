using System;
using System.Linq;
using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Infra.Common;
using Abc.Infra.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Rules {

    [TestClass]
    public class RuleElementsRepoTests 
        : RuleRepoTests<RuleElementsRepo, IRuleElement, RuleElementData> {

        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            obj = getObject(db);
        }
        [TestMethod]
        public void CreateContextElementsTest() {
            var c = GetRandom.UInt8(3, 10);
            var ruleId = rndStr;

            for (var i = 0; i < c; i++) {
                var d = GetRandom.ObjectOf<RuleElementData>();
                d.RuleId = ruleId;
                d.RuleContextId = null;
                d.Index = i + 1;
                obj.dbSet.Add(d);
            }

            obj.db.SaveChanges();

            var ruleContextId = rndStr;
            obj.CreateContextElements(ruleContextId, ruleId);

            var lRule = obj.dbSet
                .Where(x => x.RuleId == ruleId)
                .OrderBy(x => x.Index)
                .ToList();
            var lContext = obj.dbSet
                .Where(x => x.RuleContextId == ruleContextId)
                .OrderBy(x => x.Index)
                .ToList();
            Assert.AreEqual(lRule.Count, lContext.Count);

            for (var i = 0; i < c; i++) {
                Assert.AreEqual(lRule[i].RuleElementType, lContext[i].RuleElementType);
                Assert.AreEqual(lRule[i].Operation, lContext[i].Operation);
                Assert.AreEqual(lRule[i].Value, lContext[i].Value);
            }
        }

        protected override Type getBaseClass() => typeof(PagedRepo<IRuleElement, RuleElementData>);
        protected override RuleElementsRepo getObject(RuleDb c) => new(c);
        protected override DbSet<RuleElementData> getSet(RuleDb c) => c.RuleElements;
        [TestMethod] public void GetNextElementIndexTest() {
            Assert.AreEqual(1, obj.GetNextElementIndex(true, rndStr));
            Assert.AreEqual(1, obj.GetNextElementIndex(false, rndStr));
            testGetNext(false, rndStr);
            testGetNext(true, rndStr);
        }
        private void testGetNext(bool isRuleElement, string masterId) {
            var c = GetRandom.UInt8(10, 20);
            for (var i = 0; i < c; i++) {
                var e = GetRandom.ObjectOf<RuleElementData>();
                e.Index = i;
                if (isRuleElement) e.RuleId = masterId;
                else e.RuleContextId = masterId;
                db.RuleElements.Add(e);
                db.SaveChanges();
            }
            Assert.AreEqual(c + 1, obj.GetNextElementIndex(isRuleElement, masterId));
        }
    }
}