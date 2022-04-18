using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Orders.Delivery;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Delivery {
    [TestClass]
    public class ProductDeliveryTests :SealedTests<ProductDelivery,
        Entity<ProductDeliveryData>> {

        protected override ProductDelivery createObject()
            => new(random<ProductDeliveryData>());
        [TestMethod] public void DeliveryTypeIdTest() => isReadOnly(obj.Data.DeliveryTypeId);


        [TestMethod]
        public async Task DeliveryTypeTest()
             => await testItemAsync<DeliveryTypeData, DeliveryType, IDeliveryTypesRepo>(
                 obj.DeliveryTypeId, () => obj.DeliveryType.Data, d => new DeliveryType(d));
    }
}
