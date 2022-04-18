using Abc.Domain.Common;
using Abc.Infra.Classifiers;
using Abc.Infra.Classifiers.Initializers;
using Abc.Infra.Currencies;
using Abc.Infra.Inventory;
using Abc.Infra.Orders;
using Abc.Infra.Parties;
using Abc.Infra.Parties.Initializers;
using Abc.Infra.Processes;
using Abc.Infra.Processes.Initializers;
using Abc.Infra.Products;
using Abc.Infra.Products.Initializers;
using Abc.Infra.Quantities;
using Abc.Infra.Roles;
using Abc.Infra.Rules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Abc.Soft {

    public class Program {

        public static void Main(string[] args) {
            var host = CreateHostBuilder(args)
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .Build();
            GetRepo.SetServiceProvider(host.Services);
            _= Task.Run(() => tryInitializeDatabase(host));
            host.Run();
        }

        private static void tryInitializeDatabase(IHost host) {
            try {
                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;
                var dbClassificator = services.GetRequiredService<ClassifierDb>();
                ClassifierDbInitializer.Initialize(dbClassificator);
                var dbRule = services.GetRequiredService<RuleDb>();
                RuleDbInitializer.Initialize(dbRule);
                var dbMoney = services.GetRequiredService<MoneyDb>();
                MoneyDbInitializer.Initialize(dbMoney);
                var dbQuantity = services.GetRequiredService<QuantityDb>();
                QuantityDbInitializer.Initialize(dbQuantity);
                var dbRole = services.GetRequiredService<RoleDb>();
                RoleDbInitializer.Initialize(dbRole);
                var dbParty = services.GetRequiredService<PartyDb>();
                PartyDbInitializer.Initialize(dbParty);
                var dbProduct = services.GetRequiredService<ProductDb>();
                ProductDbInitializer.Initialize(dbProduct);
                var dbOrder = services.GetRequiredService<OrderDb>();
                OrderDbInitializer.Initialize(dbOrder);
                var dbProcess = services.GetRequiredService<ProcessDb>();
                ProcessDbInitializer.Initialize(dbProcess);
                var dbInventory = services.GetRequiredService<InventoryDb>();
                InventoryDbInitializer.Initialize(dbInventory);
            } catch { }
        }

        private static TContext getContext<TContext>(IHost host) where TContext: DbContext{
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            return services.GetRequiredService<TContext>();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                    .ConfigureLogging(logging => { logging.AddAzureWebAppDiagnostics(); })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
