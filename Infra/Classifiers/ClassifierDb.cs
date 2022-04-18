using Abc.Data.Classifiers;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Classifiers {
    public class ClassifierDb :BaseDb<ClassifierDb> {
        public DbSet<ClassifierData> Classifiers { get; set; }
        public ClassifierDb(DbContextOptions<ClassifierDb> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            InitializeTables(builder);
        }
        public static void InitializeTables(ModelBuilder b) {
            if (b is null) return;
            toTable<ClassifierData>(b, nameof(Classifiers), "Classifier");
        }
    }
}