using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Statuses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Statuses {
    public abstract class OrderStatusesTests<TClass, TBaseClass> :SealedTests<TClass, TBaseClass>
        where TClass : OrderStatus {

        internal mockOrder testOrder;

        internal class mockOrder :MockOrder {
            public bool isClosable;
            public bool isCancellable;
            public override bool IsClosable() {
                base.IsClosable();
                return isClosable;
            }
            public override bool IsCancellable() {
                base.IsCancellable();
                return isCancellable;
            }
        }

        protected OrderStatus actualSender;
        protected IOrderStatus actualStatus;
        protected OrderEvent testEvent;
        protected OpenOrderEvent openEvent;
        protected CloseOrderEvent closeEvent;
        protected CancelOrderEvent cancelEvent;
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            obj.OnStatusChanged += onStatusChanged;
            testOrder = new mockOrder();
            testEvent = createEvent<OrderEvent>((OrderEventType)GetRandom.UInt8(4));
            openEvent = createEvent<OpenOrderEvent>(OrderEventType.OpenEvent);
            closeEvent = createEvent<CloseOrderEvent>(OrderEventType.CloseEvent);
            cancelEvent = createEvent<CancelOrderEvent>(OrderEventType.CancelEvent);
            isNotNull(testEvent);
            isNotNull(openEvent);
            isNotNull(closeEvent);
            isNotNull(cancelEvent);
        }
        [TestMethod] public void ErrorTest() => testStatus(null);

        private static T createEvent<T>(OrderEventType t) where T : OrderEvent =>
            LifecycleEventTests.createEvent<T>(t);
        private void onStatusChanged(object sender, IOrderStatus e) {
            actualSender = sender as OrderStatus;
            actualStatus = e;
        }
        protected void testStatus(LifecycleEvent e, OrderLifecycleStatus s, bool? closable, bool? cancellable) {
            if ((closable is null) && (cancellable is null)) test(e, s); else test(e);
            obj.order = testOrder;
            testOrder.isClosable = getValue(closable);
            testOrder.isCancellable = getValue(cancellable);
            test(e, s);
            testOrder.isClosable = !getValue(closable);
            if (closable is null) test(e, s); else test(e);
            testOrder.isCancellable = !getValue(cancellable);
            if ((closable is null) && (cancellable is null)) test(e, s); else test(e);
            testOrder.isClosable = getValue(closable);
            if (cancellable is null) test(e, s); else test(e);
        }

        private static bool getValue(bool? closable) {
            if (closable is null) return true;
            return closable.HasValue && closable.Value;
        }

        protected void testStatus(LifecycleEvent e) {
            test(e);
            obj.order = testOrder;
            test(e);
            testOrder.isClosable = true;
            test(e);
            testOrder.isCancellable = true;
            test(e);
            testOrder.isClosable = false;
            test(e);
        }
        private void test(LifecycleEvent e, OrderLifecycleStatus s) {
            clearTest();
            obj.ProcessOrderEvent(e);
            areEqual(e.Id, actualStatus.OrderEventId);
            areEqual(s, actualStatus.Status);
            areEqual(obj.Id, actualSender.Id);
        }
        private void test(LifecycleEvent e) {
            clearTest();
            obj.ProcessOrderEvent(e);
            isNull(actualStatus);
            isNull(actualSender);
        }

        private void clearTest() {
            testOrder?.Mock?.Parameters?.Clear();
            testOrder?.Mock?.CalledMethods?.Clear();
            actualSender = null;
            actualStatus = null;
        }
    }
}
