using Abc.Core.Loinc.Data;
using Abc.Core.Loinc.Infra;
using Abc.Domain.Classifiers;
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
using Abc.Soft.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Abc.Soft {

    public class Startup {
        private static string connectionString => "DefaultConnection";
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            registerDbContexts(services);
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor(); //This is for blazor
            //RegisterLoinc.Register(services, options => options.UseSqlServer(Configuration.GetConnectionString(RegisterLoinc.loincConnectionString)));
            registerRepositories(services);
        }
        private static void registerRepositories(IServiceCollection c) {
            c.AddTransient<IClassifiersRepo, ClassifiersRepo>();
            MoneyDbRepos.Register(c);
            InventoryDbRepos.Register(c);
            OrderDbRepos.Register(c);
            PartyDbRepos.Register(c);
            ProcessDbRepos.Register(c);
            ProductDbRepos.Register(c);
            QuantityDbRepos.Register(c);
            RoleDbRepos.Register(c);
            RuleDbRepos.Register(c);
        }

        protected virtual void registerDbContexts(IServiceCollection c) {
            registerDbContext<ApplicationDbContext>(c);
            registerDbContext<ClassifierDb>(c);
            registerDbContext<MoneyDb>(c);
            registerDbContext<InventoryDb>(c);
            registerDbContext<OrderDb>(c);
            registerDbContext<PartyDb>(c);
            registerDbContext<ProcessDb>(c);
            registerDbContext<ProductDb>(c);
            registerDbContext<QuantityDb>(c);
            registerDbContext<RoleDb>(c);
            registerDbContext<RuleDb>(c);
        }
        protected virtual void registerDbContext<T>(IServiceCollection services) where T : DbContext {
            services.AddDbContext<T>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(connectionString)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub(); //This is for blazor
            });
        }
    }

}
