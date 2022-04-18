using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Common;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class OrderViewFactoryTests :SealedTests<OrderViewFactory,
        AbstractViewFactory<
        OrderData, IOrder, OrderView>> {
        private readonly string[] doNotValidate = {
            nameof(OrderView.DatePurchaseOrderReceived),
            nameof(OrderView.DateSent)
        };
        [TestMethod] public void CreateTest() { }

        [TestMethod] public void CreateObjectTest() {
            var view = random<OrderView>();
            var data = new OrderViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data, doNotValidate);
            if (view.OrderType == OrderType.SalesOrder) areEqual(view.DateSent, data.DateSentReceived);
            else areEqual(view.DatePurchaseOrderReceived, data.DateSentReceived);
        }
        [TestMethod] public void CreateViewTest() {
            var data = random<OrderData>();
            var view = new OrderViewFactory().Create(OrderFactory.Create(data));

            allPropertiesAreEqual(view, data, doNotValidate);
            areEqual(view.DateSent, data.DateSentReceived);
            areEqual(view.DatePurchaseOrderReceived, data.DateSentReceived);
        }

    }
}
