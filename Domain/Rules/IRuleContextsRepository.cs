using Abc.Domain.Common;

namespace Abc.Domain.Rules {

    public interface IRuleContextsRepo : IRepo<RuleContext> {

        string GetRuleId(string id);

    }

}
