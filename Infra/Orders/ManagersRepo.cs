using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Domain.Orders;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {
    public sealed class ManagersRepo :PagedRepo<IManager, ManagerData>,
        IManagersRepo {

        public ManagersRepo(OrderDb c = null) : base(c, c?.OrderManagers) { }

        protected internal override IManager toDomainObject(ManagerData d) => ManagersFactory.Create(d);
    }
}