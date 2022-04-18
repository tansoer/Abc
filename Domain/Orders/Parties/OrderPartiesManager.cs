using Abc.Domain.Orders.Lines;
using System.Collections.Generic;

namespace Abc.Domain.Orders.Parties {
    public interface IOrderPartiesManager :IOrderManager {
        bool Amend(AmendPartySummaryEvent e);
        IReadOnlyList<OrderLineReceiver> LineReceivers { get; }
        Purchaser Purchaser { get; }
        OrderInitiator OrderInitiator { get; }
        PaymentReceiver PaymentReceiver { get; }
        OrderReceiver DeliveryReceiver { get; }
        Vendor Vendor { get; }
        SalesAgent SalesAgent { get; }
        OrderLineReceiver LineReceiver(OrderLine l);
    }
    public sealed class OrderPartiesManager : IOrderPartiesManager {
        private readonly IInternalOrderPartiesManager manager;
        internal OrderPartiesManager(IInternalOrderPartiesManager m) => manager = m;
        public OrderPartiesManager(IOrder o) => manager = new orderPartiesManager(o);        
        public OrderPartiesManager(): this((IOrder)null) { }

        public Purchaser Purchaser => getRole<Purchaser>(manager?.Purchaser);
        public OrderInitiator OrderInitiator => getRole<OrderInitiator>(manager?.OrderInitiator);
        public PaymentReceiver PaymentReceiver => getRole<PaymentReceiver>(manager?.PaymentReceiver);
        public OrderReceiver DeliveryReceiver => getRole<OrderReceiver>(manager?.DeliveryReceiver);
        public IReadOnlyList<OrderLineReceiver> LineReceivers => manager?.LineReceivers?? 
            new List<OrderLineReceiver>().AsReadOnly();
        public OrderLineReceiver LineReceiver(OrderLine l) => getRole<OrderLineReceiver>(manager.LineReceiver(l));
        public Vendor Vendor => getRole<Vendor>(manager.Vendor);
        public SalesAgent SalesAgent => getRole<SalesAgent>(manager.SalesAgent);
        public bool Amend(AmendPartySummaryEvent e) {
            if (!manager.IsCorrect(e)) return false;
            if (!manager.IsOrderCorrect(e)) return false;
            if (!manager.IsOrderLineCorrect(e)) return false;
            if (!manager.IsSignatureCorrect(e)) return false;
            if (!manager.IsEventCorrect(e)) return false;
            if (manager.IsNewEvent(e)) return addNew(e);
            if (manager.IsRemoveEvent(e)) return remove(e);
            return replace(e);
        }
        private static T getRole<T>(PartyInOrder r) where T : PartyInOrder, new() => (r as T) ?? new T();

        internal bool replace(AmendPartySummaryEvent e) =>
           manager.IsReplaceCorrect(e)
           && manager.CanRemove(e.OldPartySummary)
           && manager.Remove(e.OldPartySummary)
           && manager.CanAdd(e.PartySummary)
           && manager.Add(e.PartySummary)
           && manager.Add(e.Authorization);
        internal bool remove(AmendPartySummaryEvent e) =>
            manager.IsRemoveCorrect(e)
            && manager.CanRemove(e.OldPartySummary)
            && manager.Remove(e.OldPartySummary)
            && manager.Add(e.Authorization);
        internal bool addNew(AmendPartySummaryEvent e) =>
            manager.IsAddNewCorrect(e)
            && manager.CanAdd(e.PartySummary)
            && manager.Add(e.PartySummary)
            && manager.Add(e.Authorization);
    }
}