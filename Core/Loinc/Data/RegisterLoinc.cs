using Abc.Core.Loinc.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Abc.Core.Loinc.Data
{
    public static class RegisterLoinc {
        public static string loincConnectionString => "LoincConnection";
        public static void Register(IServiceCollection services, Action<DbContextOptionsBuilder> options) {
            services.AddDbContext<LoincContext>(options);
            services.AddScoped<DbContext, LoincContext>();
            services.AddScoped<ILoincRepo, LoincRepo>();
        }
    }
}