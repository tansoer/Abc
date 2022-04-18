using Abc.Domain.Orders;
using System.Collections.Generic;

namespace Abc.Tests.Domain.Orders {
    public sealed class MockEventsManager : MockOrderManager, IOrderEventsManager {
        public MockEventsManager(IOrder o) : base(o) { }
        public IReadOnlyList<IOrderEvent> AuditTrail 
            => registerMethod<IReadOnlyList<IOrderEvent>>();
        public bool RegisterEvent(IOrderEvent e, bool accept) 
            => registerMethod<bool>(e, accept);
    }
}