using Abc.Core.Loinc.Response;
using Microsoft.EntityFrameworkCore;

namespace Abc.Core.Loinc.Data {
    public class LoincContext : DbContext {
        public LoincContext(DbContextOptions<LoincContext> options) : base(options){ }
        
        public DbSet<LoincResponse> LoincSet { get; set; }
    }
}
