using Abc.Domain.Rules;
using Microsoft.Extensions.DependencyInjection;

namespace Abc.Infra.Rules {

    public static class RuleDbRepos {

        public static void Register(IServiceCollection services) {
            services.AddTransient<IRuleElementsRepo, RuleElementsRepo>();
            services.AddTransient<IRulesRepo, RulesRepo>();
            services.AddTransient<IRuleOverridesRepo, RuleOverridesRepo>();
            services.AddTransient<IRuleSetsRepo, RuleSetsRepo>();
            services.AddTransient<IRuleContextsRepo, RuleContextsRepo>();
            services.AddTransient<IRuleUsagesRepo, RuleUsagesRepo>();
        }

    }

}
