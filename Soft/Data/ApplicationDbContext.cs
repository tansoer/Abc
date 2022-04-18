using Abc.Infra.Classifiers;
using Abc.Infra.Currencies;
using Abc.Infra.Inventory;
using Abc.Infra.Orders;
using Abc.Infra.Parties;
using Abc.Infra.Processes;
using Abc.Infra.Products;
using Abc.Infra.Quantities;
using Abc.Infra.Roles;
using Abc.Infra.Rules;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Abc.Soft.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> o)
            : base(o) {
        }

        protected override void OnModelCreating(ModelBuilder b) {
            base.OnModelCreating(b);
            initializeTables(b);
        }

        internal static void initializeTables(ModelBuilder b) {
            ClassifierDb.InitializeTables(b);
            MoneyDb.InitializeTables(b);
            InventoryDb.InitializeTables(b);
            OrderDb.InitializeTables(b);
            PartyDb.InitializeTables(b);
            ProductDb.InitializeTables(b);
            QuantityDb.InitializeTables(b);
            RuleDb.InitializeTables(b);
            RoleDb.InitializeTables(b);
            ProcessDb.InitializeTables(b);
        }
    }
}
