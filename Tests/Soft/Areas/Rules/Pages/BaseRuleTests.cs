using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Data.Rules;
using Abc.Facade.Common;
using Abc.Infra.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Rules.Pages {

    public abstract class BaseRuleTests<TView, TData> : BaseAcceptanceTests<TView, TData, RuleDb>
        where TData : EntityBaseData where TView : EntityBaseView {
        [TestCleanup] public override void TestCleanup() {
            clear(db.RuleElements);
            clear(db.RuleOverrides);
            clear(db.RuleContexts);
            clear(db.RuleUsages);
            clear(db.Rules);
            clear(db.RuleSets);
            base.TestCleanup();
        }
        protected void addSet(string id) {
            var t = GetRandom.ObjectOf<RuleSetData>();
            t.Id = id;
            addToDatabase(t);
        }
        protected void addRule(string id) {
            var t = GetRandom.ObjectOf<RuleData>();
            t.RuleKind = RuleKind.ActivityRule;
            t.Id = id;
            addToDatabase(t);
        }
        protected void addOverride(string id) {
            var t = GetRandom.ObjectOf<RuleOverrideData>();
            t.Id = id;
            addToDatabase(t);
        }
        protected void addRuleUsage(string setId, string ruleId) {
            var t = GetRandom.ObjectOf<RuleUsageData>();
            t.RuleSetId = setId;
            t.RuleId = ruleId;
            addToDatabase(t);
        }
        protected void addRuleContext(string id) => addRandomRecord<RuleContextData>(id);
    }
}
