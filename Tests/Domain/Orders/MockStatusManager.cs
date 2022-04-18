using Abc.Domain.Orders;
using Abc.Domain.Orders.Statuses;
using System.Collections.Generic;

namespace Abc.Tests.Domain.Orders {
    public sealed class MockStatusManager :MockOrderManager, IOrderStatusManager {
        public MockStatusManager(IOrder o) : base(o) { }
        public IReadOnlyList<IOrderStatus> AuditTrail => throw new System.NotImplementedException();

        public IOrderStatus CurrentStatus => registerMethod<IOrderStatus>();

        public bool IsInitialized() {
            throw new System.NotImplementedException();
        }

        public bool ProcessEvent(ILifecycleEvent e) => registerMethod<bool>(e);
    }
}