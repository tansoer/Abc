using Abc.Data.Classifiers;
using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Data.Rules;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Parties;
using Abc.Domain.Orders.Payments;
using Abc.Domain.Orders.Statuses;
using Abc.Domain.Orders.Terms;
using Abc.Domain.Rules;
using Abc.Tests.Domain.Common;
using Abc.Tests.Domain.Orders.Lines;
using Abc.Tests.Domain.Orders.Terms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders {

    internal class MockOrder :EntityTests.MockEntity<OrderData>, IOrder {
        public string OrderManagerId => Mock.Func<string>();
        public IOrderStatusManager StatusManager => Mock.Func<IOrderStatusManager>();
        public IOrderPartiesManager PartiesManager => Mock.Func<IOrderPartiesManager>();
        public IOrderPaymentsManager PaymentsManager => Mock.Func<IOrderPaymentsManager>();
        public IOrderEventsManager EventsManager => Mock.Func<IOrderEventsManager>();
        public IOrderLinesManager LinesManager => Mock.Func<IOrderLinesManager>();
        public ITermsAndConditionsManager TermsManager => Mock.Func<ITermsAndConditionsManager>();
        public IOrderDeliveryManager DeliveryManager => Mock.Func<IOrderDeliveryManager>();
        public OrderLineReceiver LineReceiver(OrderLine l) => Mock.Func<OrderLineReceiver>(l);
        public virtual bool IsCancellable() => Mock.Func<bool>();
        public virtual bool IsClosable() => Mock.Func<bool>();
        public bool IsProductLine(IOrderLine l) => Mock.Func<bool>(l);
        public bool IsInitialized() {
            throw new System.NotImplementedException();
        }
    }
    [TestClass] public class OrderTests :AbstractTests<Order, Entity<OrderData>> {
        private class testClass :Order {
            public testClass(OrderData d) :base(d) { }
            public string Method { get; private set; }
            public object Param { get; private set; }
            protected internal override bool processDiscount(IDiscountEvent e) => process(e, nameof(processDiscount));
            protected internal override bool processDispatch(IDispatchEvent e) => process(e, nameof(processDispatch));
            protected internal override bool acceptRefund(AcceptRefundEvent e) => process(e, nameof(acceptRefund));
            protected internal override bool acceptPayment(AcceptPaymentEvent e) => process(e, nameof(acceptPayment));
            protected internal override bool makeRefund(MakeRefundEvent e) => process(e, nameof(makeRefund));
            protected internal override bool makePayment(MakePaymentEvent e) => process(e, nameof(makePayment));
            protected internal override bool processInvoice(InvoiceEvent e) => process(e, nameof(processInvoice));
            protected internal override bool processReceipt(IReceiptEvent e) => process(e, nameof(processReceipt));
            private bool process(object e, string method) {
                Method = method;
                Param = e;
                return false;
            }
        }
        protected override Order createObject() => new testClass(random<OrderData>());
        [TestMethod] public void IsClosableTest() => areEqual(false, obj.IsClosable());
        [TestMethod] public void IsCancellableTest() => areEqual(false, obj.IsCancellable());
        [TestMethod] public void DateCreatedTest() => isReadOnly(obj.ValidFrom);
        [TestMethod] public void SalesChannelIdTest() => isReadOnly(obj.Data.SalesChannelId);
        [TestMethod] public void OrderManagerIdTest() => isReadOnly(obj.Data.OrderManagerId);
        [TestMethod] public async Task OrdersManagerTest() => 
            await testItemAsync<ManagerData,IManager, IManagersRepo>(obj.OrderManagerId,
                () => obj.OrdersManager.Data, ManagersFactory.Create);
        [TestMethod] public async Task SalesChannelTest() {
            var d = random<ClassifierData>();
            d.ClassifierType = ClassifierType.SalesChannel;
            d.Id = obj.SalesChannelId;
            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>(
                d, () => obj.SalesChannel.Data, ClassifierFactory.Create
            );
        }
        [TestMethod] public void DiscountRuleContextIdTest() => isReadOnly(obj.Data.DiscountRuleContextId);
        [TestMethod] public async Task DiscountRuleContextTest() =>
            await testItemAsync<RuleContextData, RuleContext, IRuleContextsRepo>(
                obj.DiscountRuleContextId,
                () => obj.DiscountRuleContext.Data,
                d => new RuleContext(d)
            );
        [TestMethod] public void TermsManagerTest() => isReadOnly();
        [TestMethod] public void LinesManagerTest() => isReadOnly();
        [TestMethod] public void EventsManagerTest() => isReadOnly();
        [TestMethod] public void StatusManagerTest() => isReadOnly();
        [TestMethod] public void PartiesManagerTest() => isReadOnly();
        [TestMethod] public void AcceptEventTest() {
            var m = new MockEventsManager(obj);
            obj.eventsManager = m;
            var e = acceptEvent(null);
            m.ValidateMethods(e, nameof(m.RegisterEvent));
        }
        [TestMethod] public void AcceptAmendTermsAndConditionsEventTest() {
            var m = new MockTermsAndConditionsManager(obj);
            obj.termsManager = m;
            var e = acceptEvent(OrderEventType.AmendTermsAndConditionsEvent);
            m.ValidateMethods(e, nameof(m.Amend));
        }
        [TestMethod] public void AcceptOpenOrderEventTest() {
            var m = new MockStatusManager(obj);
            obj.statusManager = m;
            var e = acceptEvent(OrderEventType.OpenEvent);
            m.ValidateMethods(e, nameof(m.ProcessEvent));
        }
        [TestMethod] public void AcceptCloseOrderEventTest() {
            var m = new MockStatusManager(obj);
            obj.statusManager = m;
            var e = acceptEvent(OrderEventType.CloseEvent);
            m.ValidateMethods(e, nameof(m.ProcessEvent));
        }
        [TestMethod] public void AcceptCancelOrderEventTest() {
            var m = new MockStatusManager(obj);
            obj.statusManager = m;
            var e = acceptEvent(OrderEventType.CancelEvent);
            m.ValidateMethods(e, nameof(m.ProcessEvent));
        }
        [TestMethod] public void AcceptAmendOrderLineEventTest() {
            var m = new MockLinesManager(obj);
            obj.linesManager = m;
            var e = acceptEvent(OrderEventType.AmendOrderLineEvent);
            m.ValidateMethods(e, nameof(m.Amend));
        }
        [TestMethod] public void AcceptAmendPartySummaryEventTest() {
            var m = new MockPartiesManager(obj);
            obj.partiesManager = m;
            var e = acceptEvent(OrderEventType.AmendPartySummaryEvent);
            m.ValidateMethods(e, nameof(m.Amend));
        }
        [TestMethod] public void AcceptDiscountEventTest() {
            var e = acceptEvent(OrderEventType.DiscountEvent);
            validate(e, nameof(obj.processDiscount));
        }
        [TestMethod] public void AcceptDispatchEventTest() {
            var e = acceptEvent(OrderEventType.DispatchEvent);
            validate(e, nameof(obj.processDispatch));
        }
        [TestMethod] public void AcceptReceiptEventTest() {
            var e = acceptEvent(OrderEventType.ReceiptEvent);
            validate(e, nameof(obj.processReceipt));
        }
        [TestMethod] public void AcceptInvoiceEventTest() {
            var e = acceptEvent(OrderEventType.InvoiceEvent);
            validate(e, nameof(obj.processInvoice));
        }
        [TestMethod] public void AcceptMakePaymentEventTest() {
            var e = acceptEvent(OrderEventType.MakePaymentEvent);
            validate(e, nameof(obj.makePayment));
        }
        [TestMethod] public void AcceptAcceptPaymentEventTest() {
            var e = acceptEvent(OrderEventType.AcceptPaymentEvent);
            validate(e, nameof(obj.acceptPayment));
        }
        [TestMethod] public void AcceptMakeRefundEventTest() {
            var e = acceptEvent(OrderEventType.MakeRefundEvent);
            validate(e, nameof(obj.makeRefund));
        }
        [TestMethod] public void AcceptAcceptRefundEventTest() {
            var e = acceptEvent(OrderEventType.AcceptRefundEvent);
            validate(e, nameof(obj.acceptRefund));
        }
        [TestMethod] public void AcceptUnspecifiedOrderEventTest() {
            var m = new MockEventsManager(obj);
            obj.eventsManager = m;
            var e = acceptEvent(OrderEventType.Unspecified);
            m.ValidateMethods(e, nameof(m.RegisterEvent));
        }
        private void validate(object param, string method) {
            var t = obj as testClass;
            Assert.IsNotNull(t);
            areEqual(param, t.Param);
            areEqual(method, t.Method);
        }
        private IOrderEvent acceptEvent(OrderEventType? t) {
            var d = random<OrderEventData>();
            d.OrderEventType = t ?? OrderEventType.Unspecified;
            var e = (t is null) ? null : OrderEventFactory.Create(d);
            obj.AcceptEvent(e);
            return e;
        }
    }
}
