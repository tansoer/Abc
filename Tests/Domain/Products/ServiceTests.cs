using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {
    [TestClass] public class ServiceTests :SealedTests<Service, BaseProduct<ServiceType>> {
        [TestMethod] public async Task TypeTest() {
            isReadOnly();
            var d = GetRandom.ObjectOf<ProductTypeData>();
            d.ProductKind = ProductKind.Service;
            d.Id = obj.TypeId;
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                d, () => obj.Type.Data, ProductTypeFactory.Create);
        }

        [TestMethod] public void DeliveredFromTest() => isReadOnly(obj.Data.DeliveredFrom);
        [TestMethod] public void DeliveredToTest() => isReadOnly(obj.Data.DeliveredTo);
        [TestMethod] public void ScheduledFromTest() => isReadOnly(obj.Data.ScheduledFrom);
        [TestMethod] public void ScheduledToTest() => isReadOnly(obj.Data.ScheduledTo);
        [TestMethod] public void DeliveryStatusTest() => isReadOnly(obj.Data.DeliveryStatus);
    }
}