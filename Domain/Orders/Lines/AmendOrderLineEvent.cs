using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Parties.Signatures;
using System;
using System.Collections.Generic;

namespace Abc.Domain.Orders.Lines {
    public sealed class AmendOrderLineEvent :AmendEvent {
        internal string orderEventId = nameOf<ReturnedItem>(x => x.OrderEventId);
        private readonly IOrderLine orderLine;
        private readonly IOrderLine oldOrderLine;

        public AmendOrderLineEvent() : this(null) { }
        public AmendOrderLineEvent(OrderEventData d) : base(d) { }
        public AmendOrderLineEvent(string orderId, PartySignature s, IOrderLine orderLine = null, IOrderLine oldOrderLine = null) : base(null) {
            this.orderLine = orderLine;
            this.oldOrderLine = oldOrderLine;
            signature = s;
            var d = new OrderEventData {
                IsProcessed = false,
                OrderLineId = orderLine?.Id,
                OldOrderLineId = oldOrderLine?.Id,
                AuthorizedPartySignatureId = s?.Id,
                OrderId = orderId,
                ValidFrom = DateTime.Now
            };
            SetData(d);
        }

        internal string oldOrderLineId => get(Data?.OldOrderLineId);
        internal string orderLineId => get(Data?.OrderLineId);

        public IOrderLine OldOrderLine => oldOrderLine ?? item<IOrderLinesRepo, IOrderLine>(oldOrderLineId);
        public IOrderLine OrderLine => orderLine ?? item<IOrderLinesRepo, IOrderLine>(orderLineId);

        public IReadOnlyList<ReturnedItem> ReturnedItems => list<IReturnedItemsRepo, ReturnedItem>(orderEventId, Id);

        public override bool IsNewEvent => OldOrderLine.IsUnspecified && !OrderLine.IsUnspecified;
        public override bool IsRemoveEvent => !OldOrderLine.IsUnspecified && OrderLine.IsUnspecified;
        public bool IsCorrect(OrderLineType t) => IsAuthorized && !IsProcessed && IsTypeOf(t);
        public bool IsTypeOf(OrderLineType t) {
            var lineTypeIs = OrderLine.IsTypeOf(t);
            var oldLineTypeIs = OldOrderLine.IsTypeOf(t);
            if (lineTypeIs && oldLineTypeIs) return true;
            if (OrderLine.IsUnspecified && oldLineTypeIs) return true;
            return lineTypeIs && OldOrderLine.IsUnspecified;
        }
    }
}
