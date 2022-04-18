using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public static class RuleFactory {

        public static BaseRule Create(RuleData d) {
            return d?.RuleKind switch
            {
                RuleKind.Rule => new Rule(d),
                RuleKind.ActivityRule => new ActivityRule(d),
                _ => new Rule()
            };

        }

    }

}
