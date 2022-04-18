using Abc.Data.Orders;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Orders;
using Abc.Domain.Common;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Parties;
using Abc.Domain.Orders.Payments;
using Abc.Domain.Orders.Statuses;
using Abc.Domain.Orders.Terms;
using Abc.Domain.Rules;
using System;

namespace Abc.Domain.Orders {

    public interface IOrder :IEntity<OrderData> {
        string OrderManagerId { get; }
        bool IsClosable();
        bool IsCancellable();
        bool IsInitialized();
        IOrderStatusManager StatusManager { get;}
        IOrderPartiesManager PartiesManager { get; }
        IOrderPaymentsManager PaymentsManager { get; }
        IOrderEventsManager EventsManager { get; }
        IOrderLinesManager LinesManager { get; }
        ITermsAndConditionsManager TermsManager { get; }
        IOrderDeliveryManager DeliveryManager { get; }
        OrderLineReceiver LineReceiver(OrderLine l);
        bool IsProductLine(IOrderLine orderLine);
    }

    public interface IOrdersRepo :IRepo<IOrder> { }

    public abstract class Order :Entity<OrderData>, IOrder {
        protected Order() :this(null) { }
        protected Order(OrderData d) :base(d) {}
        internal IOrderPartiesManager partiesManager;
        internal IOrderEventsManager eventsManager;
        internal IOrderLinesManager linesManager;
        internal ITermsAndConditionsManager termsManager;
        internal IOrderStatusManager statusManager;
        internal IOrderPaymentsManager paymentsManager;
        internal IOrderDeliveryManager deliveryManager;
        public IOrderDeliveryManager DeliveryManager => deliveryManager ??= new OrderDeliveryManager(this);
        public IOrderPaymentsManager PaymentsManager => paymentsManager??= new OrderPaymentsManager(this);
        public IOrderPartiesManager PartiesManager => partiesManager??= new OrderPartiesManager(this);
        public IOrderEventsManager EventsManager => eventsManager??= new OrderEventsManager(this);
        public IOrderLinesManager LinesManager => linesManager??= new OrderLinesManager(this);
        public ITermsAndConditionsManager TermsManager => termsManager??= new TermsAndConditionsManager(this);
        public IOrderStatusManager StatusManager => statusManager??= new OrderStatusManager(this);
        public OrderLineReceiver LineReceiver(OrderLine l) => PartiesManager.LineReceiver(l);
        public string OrderManagerId => get(Data?.OrderManagerId);
        public string DiscountRuleContextId => get(Data?.DiscountRuleContextId);
        public string SalesChannelId => get(Data?.SalesChannelId);
        public DateTime DateCreated => ValidFrom;
        public virtual bool IsClosable() => false;
        public virtual bool IsCancellable() => false;
        public bool IsInitialized() => StatusManager.IsInitialized();
        public OrdersManager OrdersManager => item<IManagersRepo, IManager>(OrderManagerId) as OrdersManager;
        public SalesChannel SalesChannel => item<IClassifiersRepo, IClassifier>(SalesChannelId) as SalesChannel;
        public RuleContext DiscountRuleContext => item<IRuleContextsRepo, RuleContext>(DiscountRuleContextId);
        public bool AcceptEvent(IOrderEvent e) {
            var accept = e switch {
                IDispatchEvent x => processDispatch(x),
                IReceiptEvent x => processReceipt(x),
                IPaymentEvent x => processPayment(x),
                IDiscountEvent x => processDiscount(x),
                ILifecycleEvent x => processLifecycle(x),
                IAmendEvent x => processAmend(x),
                _ => false
            };
            return EventsManager?.RegisterEvent(e, accept)?? false;
         }
        internal bool processAmend(IAmendEvent e) => e switch {
                AmendOrderLineEvent x => LinesManager.Amend(x),
                AmendPartySummaryEvent x => PartiesManager.Amend(x),
                AmendTermsAndConditionsEvent x => TermsManager.Amend(x),
                _ => false
            }; 
        internal bool processPayment(IPaymentEvent e)
            => e switch {
                InvoiceEvent x => processInvoice(x),
                MakePaymentEvent x => makePayment(x),
                MakeRefundEvent x => makeRefund(x),
                AcceptPaymentEvent x => acceptPayment(x),
                AcceptRefundEvent x => acceptRefund(x),
                _ => false
            };
        internal bool processLifecycle(ILifecycleEvent e) => StatusManager.ProcessEvent(e);
        protected internal virtual bool processDiscount(IDiscountEvent e) => false;
        protected internal virtual bool processDispatch(IDispatchEvent e) => false;
        protected internal virtual bool acceptRefund(AcceptRefundEvent e) => false;
        protected internal virtual bool acceptPayment(AcceptPaymentEvent e) => false;
        protected internal virtual bool makeRefund(MakeRefundEvent e) => false;
        protected internal virtual bool makePayment(MakePaymentEvent e) => false;
        protected internal virtual bool processInvoice(InvoiceEvent e) => false;
        protected internal virtual bool processReceipt(IReceiptEvent e) => false;
        public bool IsProductLine(IOrderLine orderLine) => LinesManager.IsProductLine(orderLine);
    }
}