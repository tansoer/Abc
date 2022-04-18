using Abc.Data.Currencies;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Currencies {

    public class MoneyDb : BaseDb<MoneyDb> {
        public MoneyDb(DbContextOptions<MoneyDb> o): base(o) { }
        public DbSet<ExchangeRuleData> RateRules { get; set; }
        public DbSet<CurrencyData> Currencies { get; set; }
        public DbSet<CardAssociationData> CardAssociations { get; set; }
        public DbSet<CountryData> Countries { get; set; }
        public DbSet<RateTypeData> RateTypes { get; set; }
        public DbSet<ExchangeRateData> Rates { get; set; }
        public DbSet<CurrencyUsageData> CurrencyUsages { get; set; }
        public DbSet<PaymentMethodData> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder b) {
            base.OnModelCreating(b);
            InitializeTables(b);
        }

        public static void InitializeTables(ModelBuilder b) {
            if (b is null) return;
            var s = "Currency";
            toTable<CurrencyUsageData>(b, nameof(CurrencyUsages),s);
            toTableWithDecimalCol<PaymentMethodData>(b, nameof(PaymentMethods), s, x => x.CreditLimit);
            toTableWithDecimalCol<PaymentMethodData>(b, nameof(PaymentMethods), s, x => x.DailyLimit);
            toTableWithDecimalCol<ExchangeRateData>(b, nameof(Rates), s, x => x.Rate);
            toTable<RateTypeData>(b, nameof(RateTypes), s);
            toTable<CurrencyData>(b, nameof(Currencies), s);
            toTable<CardAssociationData>(b, nameof(CardAssociations), s);
            toTable<ExchangeRuleData>(b, nameof(RateRules), s);
        }
    }
}
