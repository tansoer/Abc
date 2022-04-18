using Abc.Data.Common;
using Abc.Domain.Common;

namespace Abc.Domain.Orders {
    public static class ManagersFactory {
        internal static IManager Create(ManagerData d) => new OrdersManager(d);
    }
}
