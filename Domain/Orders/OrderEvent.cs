using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Terms;
using Abc.Domain.Parties.Signatures;
using System;
using System.Collections.Generic;

namespace Abc.Domain.Orders {
    public interface IOrderEvent :IEntity<OrderEventData> {
        public bool IsProcessed { get; }
    }
    public interface IOrderEventsRepo :IRepo<IOrderEvent> { }

    public abstract class OrderEvent :Entity<OrderEventData>, IOrderEvent {
        internal protected static string eventIdInPartySignature => nameOf<PartySignatureData>(x => x.SignedEntityId);
        internal protected static string eventIdInOrderLine => nameOf<OrderLineData>(x => x.OrderEventId);
        internal protected PartySignature signature;
        internal protected IOrder order;

        protected OrderEvent() : this(null) { }
        protected OrderEvent(OrderEventData d) : base(d) { }
        public IReadOnlyList<IOrderLine> OrderLines
            => list<IOrderLinesRepo, IOrderLine>(eventIdInOrderLine, Id);
        public string OrderId => get(Data?.OrderId);
        public string AuthorizedPartySignatureId => get(Data?.AuthorizedPartySignatureId);
        public IOrder Order => order?? item<IOrdersRepo, IOrder>(OrderId) ?? new UnspecifiedOrder();
        public PartySignature Authorization =>
            signature ?? item<IPartySignaturesRepo, PartySignature>(AuthorizedPartySignatureId) ?? new PartySignature();
        public DateTime DateAuthorized => get(Data?.ValidFrom);
        public bool IsAuthorized => !IsUnspecified && (Authorization?.IsSigned() ?? false);
        public IReadOnlyList<PartySignature> Approvals
            => list<IPartySignaturesRepo, PartySignature>(eventIdInPartySignature, Id);
        public bool IsProcessed => get(Data?.IsProcessed);
        public bool IsCorrect() => IsAuthorized && !IsProcessed;
        protected internal PartySignature validateSignature(PartySignature s) {
            var d = s?.Data ?? new PartySignatureData();
            d.SignedEntityId = Id;
            d.SignedEntityTypeId = typeof(ITermsAndConditionsRepo).FullName;
            d.ValidFrom = DateTime.Now;
            d.ValidTo = null;
            s = new PartySignature(d);
            return s;
        }
        internal static IOrder validateOrder(IOrder s) => s ?? OrderFactory.Create(null);
        protected internal void updateData() {
            var d = new OrderEventData();
            setData(d);
            SetData(d);
        }
        protected internal virtual void setData(OrderEventData d) {
            d.Id = data.Id;
            d.IsProcessed = false;
            d.AuthorizedPartySignatureId = signature?.Id;
            d.OrderId = order.Id;
            d.ValidFrom = DateTime.Now;
            d.ValidTo = null;
        }
    }
}
