using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Roles;
using System;

namespace Abc.Domain.Orders.Parties {
    public sealed class AmendPartySummaryEvent :AmendEvent {
        internal IPartyInOrder partySummary;
        internal IPartyInOrder oldPartySummary;
        internal IOrderLine orderLine;

        public AmendPartySummaryEvent() : this(null) { }
        public AmendPartySummaryEvent(OrderEventData d) : base(d) { }
        public AmendPartySummaryEvent(IOrder o, PartySignature s, 
            IPartyInOrder partySummary = null, IPartyInOrder oldPartySummary = null) : base(null) {
            order = validateOrder(o);
            signature = validateSignature(s);
            this.partySummary = validatePartySummary(partySummary);
            this.oldPartySummary = validatePartySummary(oldPartySummary);
            updateData();
        }
        internal IPartyInOrder validatePartySummary(IPartyInOrder p) {
            var d = p?.Data ?? new PartySummaryData();
            d.OrderId = order?.Id;
            d.OrderLineId = orderLine?.Id;
            d.ValidFrom = DateTime.Now;
            d.ValidTo = null;
            p = PartyInOrderFactory.Create(d);
            return p;
        }
        public AmendPartySummaryEvent(IOrderLine l, PartySignature s, IPartyInOrder partySummary = null, IPartyInOrder oldPartySummary = null) : base(null) {
            order = validateOrder(l?.Order);
            orderLine = validateOrderLine(l);
            signature = validateSignature(s);
            this.partySummary = validatePartySummary(partySummary);
            this.oldPartySummary = validatePartySummary(oldPartySummary);
            updateData();
        }
        internal static IOrderLine validateOrderLine(IOrderLine l) => l ?? OrderLineFactory.Create(null);

        internal string partySummaryId => get(Data?.PartySummaryId);
        internal string oldPartySummaryId => get(Data?.OldPartySummaryId);
        internal string orderLineId => get(Data?.OrderLineId);

        public IOrderLine OrderLine
            => orderLine ?? item<IOrderLinesRepo, IOrderLine>(orderLineId);
        public IPartyInOrder PartySummary
            => partySummary ?? getSummary(partySummaryId);
        public IPartyInOrder OldPartySummary
            => oldPartySummary ?? getSummary(oldPartySummaryId);

        public override bool IsNewEvent => OldPartySummary.IsUnspecified && !PartySummary.IsUnspecified;
        public override bool IsRemoveEvent => !OldPartySummary.IsUnspecified && PartySummary.IsUnspecified;

        public bool IsCorrect(PartyRoleInOrder p) => IsAuthorized && !IsProcessed && IsRoleTypeOf(p);

        public bool IsRoleTypeOf(PartyRoleInOrder p) {
            var partySummaryIs = PartySummary.IsRoleTypeOf(p);
            var oldPartySummaryIs = OldPartySummary.IsRoleTypeOf(p);
            if (partySummaryIs && oldPartySummaryIs) return true;
            if (PartySummary.IsUnspecified && oldPartySummaryIs) return true;
            return partySummaryIs && OldPartySummary.IsUnspecified;
        }
        protected internal override void setData(OrderEventData d) {
            base.setData(d);
            d.OrderEventType = OrderEventType.AmendPartySummaryEvent;
            d.PartySummaryId = partySummary?.Id;
            d.OldPartySummaryId = oldPartySummary?.Id;
            d.OrderLineId = orderLine?.Id;
        }
        private static IPartyInOrder getSummary(string id)
            => item<IPartySummariesRepo, IPartySummary>(id) as PartyInOrder ?? new UnspecifiedRoleInOrder();
        public bool IsOrderLineAmend() => ! OrderLine.IsUnspecified;
    }
}
