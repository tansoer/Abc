using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Tests.Infra {
    public class DbInitializerTests<TDbContext> : TestsData
        where TDbContext : DbContext {
        protected TDbContext db { get; set; }
        protected DbContextOptions<TDbContext> options { get; set; }
        public DbInitializerTests() => useInMemoryDatabase();
        protected void removeAll<TData>(DbSet<TData> set) where TData : class, new() => removeAll(set, db);
        private void useInMemoryDatabase() => options = new DbContextOptionsBuilder<TDbContext>()
            .UseInMemoryDatabase("TestDb").Options;

        [TestMethod] public void IsSetsTested() {
            var l = getAllSets();
            if (l.Count == 0) return;
            notTested($"{l[0]} is not tested");
        }
        private List<string> getAllSets() {
            var sets = db.GetType().GetProperties();
            var names = sets
                .Where(x => x.PropertyType.Name.Contains("DbSet"))
                .Select(x => x.Name).ToList();
            var name = GetType().Name;
            if (!name.Contains("DbInitializer"))
                names.RemoveAll(x => !name.StartsWith(x));
            var tests = GetType().GetMethods().Select(x => x.Name).ToList();
            names.RemoveAll(x => tests.Exists(y => y.StartsWith(x)));
            return names;
        }
    }
}
