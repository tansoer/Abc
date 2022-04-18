using Abc.Domain.Currencies;
using Microsoft.Extensions.DependencyInjection;

namespace Abc.Infra.Currencies {

    public static class MoneyDbRepos {

        public static void Register(IServiceCollection services) {
            services.AddTransient<IPaymentMethodsRepo, PaymentMethodsRepo>();
            services.AddTransient<ICurrencyUsagesRepo, CurrencyUsagesRepo>();
            services.AddTransient<IExchangeRatesRepo, ExchangeRatesRepo>();
            services.AddTransient<IExchangeRulesRepo, ExchangeRulesRepo>();
            services.AddTransient<IRateTypesRepo, ExchangeTypesRepo>();
            services.AddTransient<ICurrencyRepo, CurrencyRepo>();
            services.AddTransient<ICardAssociationsRepo, CardAssociationsRepo>();
        }
    }
}
