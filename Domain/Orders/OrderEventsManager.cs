using Abc.Aids.Methods;
using Abc.Data.Orders;
using System;
using System.Collections.Generic;

namespace Abc.Domain.Orders {
    public interface IOrderEventsManager :IOrderManager {
        public IReadOnlyList<IOrderEvent> AuditTrail { get; }
        bool RegisterEvent(IOrderEvent e, bool accept);
    }
    public sealed class OrderEventsManager :OrderManager, IOrderEventsManager {
        internal static string orderIdInEvent => nameOf<OrderEventData>(x => x.OrderId);
        public IReadOnlyList<IOrderEvent> AuditTrail
            => list<IOrderEventsRepo, IOrderEvent>(orderIdInEvent, order?.Id);
        public OrderEventsManager(IOrder o) : base(o) { }
        public OrderEventsManager() { }
        public bool RegisterEvent(IOrderEvent e, bool accept)
            => Safe.Run(() => {
                add<IOrderEventsRepo, IOrderEvent>(
                    OrderEventFactory.Create(composeData(e, accept)));
                return accept;
            }, false);

        internal OrderEventData composeData(IOrderEvent e, bool accept) {
            var d = e?.Data ?? new OrderEventData();
            d.OrderId = order.Id;
            d.IsProcessed = accept;
            d.ValidFrom = DateTime.Now;
            return d;
        }
    }
}