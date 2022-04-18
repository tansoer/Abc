using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Orders;
using Abc.Pages.Common;
using Abc.Pages.Orders;
using Abc.Tests.Facade.Orders;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Orders {
    [TestClass] public class OrdersPageTests:
        SealedViewsPageTests<OrdersPage, IOrdersRepo, IOrder, OrderView, OrderData, OrderType> {

        protected override System.Type getBaseClass() =>
            typeof(ViewsPage<OrdersPage, IOrdersRepo, IOrder, OrderView, OrderData, OrderType>);
        protected override string pageTitle => OrderTitles.Orders;
        protected override string pageUrl => OrderUrls.Orders;
        protected override IOrder toObject(OrderData d) => OrderFactory.Create(d);
        private class testRepo :mockRepo<IOrder, OrderData>, IOrdersRepo { }

        private testRepo Repo;

        protected override OrdersPage createObject() {
            Repo = new testRepo();
            addRandomOrders();
            return new OrdersPage(Repo);
        }
        private void addRandomOrders() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                Repo.Add(OrderFactory.Create(GetRandom.ObjectOf<OrderData>()));
        }
        [TestMethod]
        public override void ToObjectTest() {
            var view = GetRandom.ObjectOf<OrderView>();
            var o = obj.toObject(view);
            allPropertiesAreEqual(view, o.Data, "DateSent", "DatePurchaseOrderReceived");
            areEqual(view.OrderType == OrderType.SalesOrder ? view.DateSent : view.DatePurchaseOrderReceived,
                o.Data.DateSentReceived);
        }
        [TestMethod]
        public override void ToViewTest() {
            var d = GetRandom.ObjectOf<OrderData>();
            var view = obj.toView(toObject(d));
            allPropertiesAreEqual(view, d, "DateSent", "DatePurchaseOrderReceived");
            areEqual(view.DateSent, d.DateSentReceived);
            areEqual(view.DatePurchaseOrderReceived, d.DateSentReceived);
        }
    }
}
