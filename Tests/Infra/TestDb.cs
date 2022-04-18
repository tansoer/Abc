using Abc.Tests.Data;
using Microsoft.EntityFrameworkCore;

namespace Abc.Tests.Infra {
    public class TestDb : DbContext {
        public DbSet<TestData> TestData { get; set; }

        public TestDb(DbContextOptions<TestDb> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            InitializeTables(builder);
        }

        public static void InitializeTables(ModelBuilder builder) 
            =>builder?.Entity<TestData>().ToTable(nameof(TestData));
    }
}
