using System;
using System.Collections.Generic;
using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Common {

    [TestClass]
    public class GetFromTests : SealedTests<GetFrom<IRuleUsagesRepo, RuleUsage>, object> {

        protected IRuleUsagesRepo Repo;
        protected string id;
        protected string ruleSetId;
        protected RuleUsageData data;
        protected RuleUsage item;
        private RuleUsage createItem() => new (data);
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            Repo = GetRepo.Instance<IRuleUsagesRepo>();
            data = GetRandom.ObjectOf<RuleUsageData>();
            id = data.Id;
            ruleSetId = rndStr;
            item = createItem();
        }
        [TestMethod] public void RepoTest() => Assert.AreNotSame(Repo, GetFrom<IRuleUsagesRepo, RuleUsage>.repo);
        [TestMethod] public void ByIdTest() {
            var t = obj.ById(id);
            Assert.IsNotNull(t);
            Assert.IsInstanceOfType(t, typeof(RuleUsage));
            Assert.IsTrue(t.IsUnspecified);
        }
        [TestMethod] public void GetByIdTest() {
            Repo.Add(item);
            allPropertiesAreEqual(data, obj.ById(id).Data);
        }
        protected void listTest() {
            var t = getList();
            Assert.IsNotNull(t);
            Assert.IsInstanceOfType(t, typeof(IReadOnlyList<RuleUsage>));
            Assert.AreEqual(0, t.Count);
        }
        protected virtual IReadOnlyList<RuleUsage> getList() => obj.ListBy("RuleSetId", ruleSetId);
        protected void contentTest() {
            listTest();
            var count = GetRandom.UInt8(10, 30);
            for (var i = 0; i < count; i++) {
                data = GetRandom.ObjectOf<RuleUsageData>();
                if (i % 4 == 0) updateData(i);
                Repo.Add(createItem());
            }
            var c = (int) Math.Ceiling(count / 4.0);
            var t = getList();
            Assert.AreEqual(c, t.Count);
        }
        private void updateData(int idx) {
            if (idx % 4 == 0) data.RuleSetId = ruleSetId;
        }
        [TestMethod] public void ListByTest() => listTest();
        [TestMethod] public void ListContextTest() => contentTest();
    }
}