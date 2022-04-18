//(c) - https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1

using System;
using System.Linq;
using Abc.Domain.Common;
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
using Abc.Soft;
using Abc.Soft.Data;
using Abc.Tests.Domain;
using Abc.Tests.Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Abc.Tests {

    public class TestHost<TStartup>
        :WebApplicationFactory<TStartup> where TStartup : class {

        private readonly Type[] dbContexts = new[] {
            typeof(ApplicationDbContext),
            typeof(ClassifierDb),
            typeof(MoneyDb),
            typeof(InventoryDb),
            typeof(OrderDb),
            typeof(PartyDb),
            typeof(ProcessDb),
            typeof(ProductDb),
            typeof(QuantityDb),
            typeof(RoleDb),
            typeof(RuleDb),
            typeof(TestDb)
        };


        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            builder.ConfigureTestServices(s => {
                removeCurrentDb<ApplicationDbContext>(s);
                removeCurrentDb<ClassifierDb>(s);
                removeCurrentDb<MoneyDb>(s);
                removeCurrentDb<InventoryDb>(s);
                removeCurrentDb<OrderDb>(s);
                removeCurrentDb<PartyDb>(s);
                removeCurrentDb<ProcessDb>(s);
                removeCurrentDb<ProductDb>(s);
                removeCurrentDb<QuantityDb>(s);
                removeCurrentDb<RoleDb>(s);
                removeCurrentDb<RuleDb>(s);
                removeCurrentDb<TestDb>(s);

                s.AddEntityFrameworkInMemoryDatabase();

                addInMemoryDb<ApplicationDbContext>(s);
                addInMemoryDb<ClassifierDb>(s);
                addInMemoryDb<MoneyDb>(s);
                addInMemoryDb<InventoryDb>(s);
                addInMemoryDb<OrderDb>(s);
                addInMemoryDb<PartyDb>(s);
                addInMemoryDb<ProcessDb>(s);
                addInMemoryDb<ProductDb>(s);
                addInMemoryDb<QuantityDb>(s);
                addInMemoryDb<RoleDb>(s);
                addInMemoryDb<RuleDb>(s);
                addInMemoryDb<TestDb>(s);

                s.AddTransient<ITestRepo, TestRepo>();

                var sp = s.BuildServiceProvider();

                using (var scope = sp.CreateScope()) {

                    var scopedServices = scope.ServiceProvider;
                    ensureDbsAreCreated(scopedServices, dbContexts);

                }

                GetRepo.SetServiceProvider(sp);
            });
        }
        private static void ensureDbsAreCreated(IServiceProvider s, Type[] types) {
            foreach (var t in types) ensureDbIsCreated(s, t);
        }
        private static void ensureDbIsCreated(IServiceProvider s, Type t) {
            if (s.GetRequiredService(t) is not DbContext db)
                throw new ApplicationException($"DBContext {t} not found");
            db.Database.EnsureCreated();
            if (!db.Database.IsInMemory())
                throw new ApplicationException($"DBContext {t} is not in memory");
        }
        private static void addInMemoryDb<T>(IServiceCollection s) where T : DbContext {
            s.AddDbContext<T>(o => { o.UseInMemoryDatabase("Tests"); });
        }
        private static void removeCurrentDb<T>(IServiceCollection s) where T : DbContext {
            var descriptor = s.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<T>));

            if (descriptor != null) { s.Remove(descriptor); }
        }
        public T GetContext<T>() where T : DbContext {
            var scope = Services.CreateScope();
            var s = scope.ServiceProvider;
            return s.GetRequiredService<T>();
        }
    }
}