using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Roles;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Domain.Orders.Parties {
    internal interface IInternalOrderPartiesManager {
        IReadOnlyList<OrderLineReceiver> LineReceivers { get; }
        PartyInOrder Purchaser { get; }
        PartyInOrder OrderInitiator { get; }
        PartyInOrder PaymentReceiver { get; }
        PartyInOrder DeliveryReceiver { get; }
        PartyInOrder Vendor { get; }
        PartyInOrder SalesAgent { get; }
        PartyInOrder LineReceiver(OrderLine l);
        bool Add(IPartyInOrder p);
        bool Add(PartySignature s);
        bool CanAdd(IPartyInOrder p);
        bool CanRemove(IPartyInOrder p);
        bool IsAddNewCorrect(AmendPartySummaryEvent e);
        bool IsRemoveCorrect(AmendPartySummaryEvent e);
        bool IsReplaceCorrect(AmendPartySummaryEvent e);
        bool Remove(IPartyInOrder p);
        bool IsCorrect(AmendPartySummaryEvent e);
        bool IsNewEvent(AmendPartySummaryEvent e);
        bool IsRemoveEvent(AmendPartySummaryEvent e);
        bool IsOrderCorrect(AmendPartySummaryEvent e);
        bool IsOrderLineCorrect(AmendPartySummaryEvent e);
        bool IsSignatureCorrect(AmendPartySummaryEvent e);
        bool IsEventCorrect(AmendPartySummaryEvent e);
    }
    internal class orderPartiesManager :OrderManager, IInternalOrderPartiesManager {
        private static string orderId => nameOf<PartySummaryData>(x => x.OrderId);
        internal OrderData data => order?.Data;
        internal IReadOnlyList<IPartySummary> partySummaries => list<IPartySummariesRepo, IPartySummary>(orderId, order.Id);
        internal IReadOnlyList<PartyInOrder> parties => list<PartyInOrder, IPartySummary>(partySummaries);

        public orderPartiesManager(IOrder o) : base(o) { }
        public orderPartiesManager() { }

        public PartyInOrder Purchaser => getPartyInRole(PartyRoleInOrder.Purchaser);
        public PartyInOrder OrderInitiator => getPartyInRole(PartyRoleInOrder.OrderInitiator);
        public PartyInOrder PaymentReceiver => getPartyInRole(PartyRoleInOrder.PaymentReceiver);
        public PartyInOrder DeliveryReceiver => getPartyInRole(PartyRoleInOrder.OrderReceiver);
        public IReadOnlyList<OrderLineReceiver> LineReceivers => list<OrderLineReceiver, PartyInOrder>(parties);
        public PartyInOrder LineReceiver(OrderLine l) {
            var p = LineReceivers.FirstOrDefault(x => x.orderLineId == l?.Id);
            return p is not null ? p : new UnspecifiedRoleInOrder();
        }
        public PartyInOrder Vendor => getPartyInRole(PartyRoleInOrder.Vendor);
        public PartyInOrder SalesAgent => getPartyInRole(PartyRoleInOrder.SalesAgent);
        public bool IsOrderLineCorrect(AmendPartySummaryEvent e) {
            if (e is null) return false;
            if (!e.IsOrderLineAmend()) return true;
            if (order.IsProductLine(e.orderLine)) return true;
            return false;
        }
        internal static PartyRoleInOrder getPartyRole<T>(T p) where T : IPartyInOrder {
            if (p is Vendor) return PartyRoleInOrder.Vendor;
            if (p is OrderInitiator) return PartyRoleInOrder.OrderInitiator;
            if (p is OrderReceiver) return PartyRoleInOrder.OrderReceiver;
            if (p is OrderLineReceiver) return PartyRoleInOrder.OrderLineReceiver;
            if (p is PaymentReceiver) return PartyRoleInOrder.PaymentReceiver;
            if (p is Purchaser) return PartyRoleInOrder.Purchaser;
            if (p is SalesAgent) return PartyRoleInOrder.SalesAgent;
            return PartyRoleInOrder.Unspecified;
        }
        internal PartyInOrder getPartyInRole(PartyRoleInOrder p)
            => parties.FirstOrDefault(x => x.RoleInOrder == p) ?? new UnspecifiedRoleInOrder();
        internal PartyInOrder getCurrentPartyInRole(IPartyInOrder p) => p switch {
            OrderReceiver => DeliveryReceiver,
            Parties.Purchaser => Purchaser,
            Parties.OrderInitiator => OrderInitiator,
            Parties.PaymentReceiver => PaymentReceiver,
            Parties.Vendor => Vendor,
            Parties.SalesAgent => SalesAgent,
            OrderLineReceiver => LineReceiver(p.OrderLine),
            _ => new UnspecifiedRoleInOrder()
        };
        internal static bool isSpecified(IPartyInOrder p) => !(p?.IsUnspecified ?? true);
        public bool CanAdd(IPartyInOrder p) => (p is not null) && !isSpecified(getCurrentPartyInRole(p));
        public bool CanRemove(IPartyInOrder p) => isSameParty(getCurrentPartyInRole(p), p);
        public bool Add(IPartyInOrder p) => add<IPartySummariesRepo, IPartySummary>(p);
        public bool Add(PartySignature s) => add<IPartySignaturesRepo, PartySignature>(s);
        public bool Remove(IPartyInOrder p) => delete<IPartySummariesRepo, IPartySummary>(p);
        public bool IsEventCorrect(AmendPartySummaryEvent e)
            => (e?.OldPartySummary?.IsCorrect() ?? false)
            || (e?.PartySummary?.IsCorrect() ?? false);
        public bool IsReplaceCorrect(AmendPartySummaryEvent e)
                => (e is not null)
                && exists(e?.OldPartySummary)
                && !exists(e?.PartySummary);
        public bool IsRemoveCorrect(AmendPartySummaryEvent e)
                => (e is not null)
                && exists(e?.OldPartySummary);
        public bool IsAddNewCorrect(AmendPartySummaryEvent e)
                => (e is not null)
                && !exists(e?.PartySummary);
        internal bool exists(IPartyInOrder t)
                => parties.FirstOrDefault(x => isSameParty(x, t)) != null;
        internal static bool isSameParty(PartyInOrder x, IPartyInOrder t) {
            var y = t as PartyInOrder;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType().Name != y.GetType().Name) return false;
            if (x.Id != t.Id) return false;
            if (x is not OrderLineReceiver) return true;
            if (x.orderLineId != y.orderLineId) return false;
            return true;
        }

        public bool IsCorrect(AmendPartySummaryEvent e) => e?.IsCorrect() ?? false;

        public bool IsNewEvent(AmendPartySummaryEvent e) => e?.IsNewEvent ?? false;

        public bool IsRemoveEvent(AmendPartySummaryEvent e) => e?.IsRemoveEvent ?? false;

        public bool IsOrderCorrect(AmendPartySummaryEvent e) => isOrderCorrect(e);

        public bool IsSignatureCorrect(AmendPartySummaryEvent e) =>isSignatureCorrect(e);
    }
}