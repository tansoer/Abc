using Abc.Data.Quantities;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Quantities {
    public class QuantityDb : BaseDb<QuantityDb> {
        public DbSet<MeasureData> Measures { get; set; }
        public DbSet<UnitData> Units { get; set; }
        public DbSet<SystemOfUnitsData> SystemsOfUnits { get; set; }
        public DbSet<UnitFactorData> UnitFactors { get; set; }
        public DbSet<CommonTermData> CommonTerms { get; set; }
        public DbSet<UnitRulesData> UnitRules { get; set; }
        public QuantityDb(DbContextOptions<QuantityDb> o) : base(o) { }
        protected override void OnModelCreating(ModelBuilder b) {
            base.OnModelCreating(b);
            InitializeTables(b);
        }
        public static void InitializeTables(ModelBuilder b) {
            if (b is null) return;
            var s = "Quantity";
            toTable<SystemOfUnitsData>(b, nameof(SystemsOfUnits), s);
            toTable<MeasureData>(b, nameof(Measures),s);
            toTable<UnitData>(b, nameof(Units),s);
            toTable<UnitFactorData>(b, nameof(UnitFactors),s);
            toTable<UnitRulesData>(b, nameof(UnitRules),s);
            toTable<CommonTermData>(b, nameof(CommonTerms),s);
        }
    }
}
