using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass]
    public class ProductDeliveryDataTests :SealedTests<ProductDeliveryData, EntityBaseData> {
        [TestMethod] public void DeliveryTypeIdTest() => isNullable<string>();
    }
}