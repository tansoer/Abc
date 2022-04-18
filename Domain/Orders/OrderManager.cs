using Abc.Domain.Common;
using Abc.Domain.Orders.Terms;

namespace Abc.Domain.Orders {
    public interface IOrderManager { }

    public abstract class OrderManager :Archetype, IOrderManager {
        protected internal IOrder order;
        protected OrderManager() : this(null) { }
        protected OrderManager(IOrder o) => order ??= o;

        protected internal bool isOrderCorrect(OrderEvent e) {
            var o = e?.Order;
            if (o is null) return false;
            if (o.IsUnspecified) return false;
            if (o.Id != order?.Id) return false;
            return true;
        }
        internal static bool isSignatureCorrect(OrderEvent e) {
            var s = e?.Authorization;
            if (s is null) return false;
            if (!s.IsSigned()) return false;
            if (s.SignedEntityId != e.Id) return false;
            return true;
        }
    }
}