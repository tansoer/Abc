using Abc.Data.Orders;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Domain.Orders.Statuses {

    public interface IOrderStatusManager: IOrderManager {
        bool ProcessEvent(ILifecycleEvent e);
        IReadOnlyList<IOrderStatus> AuditTrail { get; }
        IOrderStatus CurrentStatus { get; }
        bool IsInitialized();
    }
 
    public sealed class OrderStatusManager: OrderManager, IOrderStatusManager {
        internal static string orderIdInStatus => nameOf<OrderStatusData>(x => x.OrderId);
        internal IOrderStatus currentStatus;
        public IReadOnlyList<IOrderStatus> AuditTrail
            => list<IOrderStatusesRepo, IOrderStatus>(orderIdInStatus, order?.Id);
        public IOrderStatus CurrentStatus
            => currentStatus ?? AuditTrail.OrderByDescending(x => x.ValidFrom).FirstOrDefault();
        public bool ProcessEvent(ILifecycleEvent e) {
            if (isNull(CurrentStatus)) return false;
            CurrentStatus.OnStatusChanged += doOnOrderStatusChanged;
            return CurrentStatus.ProcessOrderEvent(e);
        }
        internal void doOnOrderStatusChanged(object sender, IOrderStatus s) {
            CurrentStatus.OnStatusChanged -= doOnOrderStatusChanged;
            add<IOrderStatusesRepo, IOrderStatus>(s);
        }
        public bool IsInitialized() => CurrentStatus.IsInitialized();
        public OrderStatusManager(IOrder o) : base(o) { }
        public OrderStatusManager() { }
    }
}
