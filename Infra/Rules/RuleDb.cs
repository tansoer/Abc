using Abc.Data.Rules;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Rules {

    public class RuleDb : BaseDb<RuleDb> {

        public DbSet<RuleElementData> RuleElements { get; set; }
        public DbSet<RuleData> Rules { get; set; }
        public DbSet<RuleOverrideData> RuleOverrides { get; set; }
        public DbSet<RuleSetData> RuleSets { get; set; }
        public DbSet<RuleContextData> RuleContexts { get; set; }
        public DbSet<RuleUsageData> RuleUsages { get; set; }

        public RuleDb(DbContextOptions<RuleDb> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            InitializeTables(builder);
        }

        public static void InitializeTables(ModelBuilder b) {
            if (b is null) return;
            var s = "Rule";
            toTable<RuleElementData>(b, nameof(RuleElements), s);
            toTable<RuleData>(b, nameof(Rules), s);
            toTable<RuleOverrideData>(b, nameof(RuleOverrides), s);
            toTable<RuleSetData>(b, nameof(RuleSets), s);
            toTable<RuleContextData>(b, nameof(RuleContexts), s);
            toTable<RuleUsageData>(b, nameof(RuleUsages), s);
        }
    }
}
