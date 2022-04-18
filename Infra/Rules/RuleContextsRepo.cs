using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Infra.Common;

namespace Abc.Infra.Rules {
    public sealed class RuleContextsRepo : EntityRepo<RuleContext, RuleContextData>,
        IRuleContextsRepo {
        public RuleContextsRepo(RuleDb c = null) : base(c, c?.RuleContexts) { }
        public string GetRuleId(string id) {
            var r = Get(id);
            return r?.RuleId;
        }
    }
}

