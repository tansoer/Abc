using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Common {

    public abstract class EntityRepo<TDomain, TData> 
        : PagedRepo<TDomain, TData>
        where TDomain : IBaseEntity<TData>, new()
        where TData : BaseData, new() {

        protected EntityRepo(DbContext c, DbSet<TData> s) : base(c, s) { }

        protected override internal TDomain toDomainObject(TData d) {
            var o = new TDomain();
            o.SetData(d);
            return o;
        }
    }
}
