using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Packets {

    [TestClass]
    public class PackageComponentTests : SealedTests<PackageComponent, Entity<PackageComponentData>> {

        protected override PackageComponent createObject() =>
            new (GetRandom.ObjectOf<PackageComponentData>());

        [TestMethod] public void PackageTypeIdTest() => isReadOnly(obj.Data.PackageTypeId);

        [TestMethod] public void ProductTypeIdTest() => isReadOnly(obj.Data.ProductTypeId);

        [TestMethod]
        public async Task ProductTypeTest() {
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                obj.ProductTypeId, () => obj.ProductType.Data, ProductTypeFactory.Create);
        }

        [TestMethod]
        public async Task PackageTypeTest() {
            var data = GetRandom.ObjectOf<ProductTypeData>();
            data.Id = obj.PackageTypeId;
            data.ProductKind = ProductKind.Package;
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                data, () => obj.PackageType.Data, ProductTypeFactory.Create);
        }

    }

}