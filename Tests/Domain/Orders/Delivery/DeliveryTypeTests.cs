using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Orders.Delivery;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Delivery {
    [TestClass]
    public class DeliveryTypeTests :SealedTests<DeliveryType,
        EntityType<IDeliveryTypesRepo, DeliveryType, DeliveryTypeData>> {

        protected override DeliveryType createObject()
            => new(random<DeliveryTypeData>());
    }
}
