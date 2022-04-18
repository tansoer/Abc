using Abc.Domain.Quantities;
using Microsoft.Extensions.DependencyInjection;

namespace Abc.Infra.Quantities {

    public static class QuantityDbRepos {

        public static void Register(IServiceCollection services) {
            services.AddTransient<IMeasuresRepo, MeasuresRepo>();
            services.AddTransient<IUnitsRepo, UnitsRepo>();
            services.AddTransient<ISystemsOfUnitsRepo, SystemsOfUnitsRepo>();
            services.AddTransient<IUnitTermsRepo, UnitTermsRepo>();
            services.AddTransient<IUnitFactorsRepo, UnitFactorsRepo>();
            services.AddTransient<IUnitRulesRepo, UnitRulesRepo>();
            services.AddTransient<IMeasureTermsRepo, MeasureTermsRepo>();
        }

    }

}
