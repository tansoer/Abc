using Abc.Facade.Common;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class ProductDeliveryViewTests 
        :SealedTests<ProductDeliveryView, EntityBaseView> {
        [TestMethod] public void DeliveryTypeIdTest() => isNullableProperty<string>("Delivery type");
    }
}