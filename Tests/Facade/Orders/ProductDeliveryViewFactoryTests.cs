using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Facade.Orders;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class ProductDeliveryViewFactoryTests :ViewFactoryTests<ProductDeliveryViewFactory,
       ProductDeliveryData, ProductDelivery, ProductDeliveryView> {
    }
}
