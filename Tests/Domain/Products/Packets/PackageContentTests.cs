using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Packets {

    [TestClass]
    public class PackageContentTests : SealedTests<PackageContent, Entity<PackageContentData>> {

        protected override PackageContent createObject() =>
            new (GetRandom.ObjectOf<PackageContentData>());

        [TestMethod] public void PackageIdTest() => isReadOnly(obj.Data.PackageId);

        [TestMethod] public void ProductIdTest() => isReadOnly(obj.Data.ProductId);

        [TestMethod] public async Task ProductTest() {

            await testItemAsync<ProductData, IProduct, IProductsRepo>(
                obj.ProductId, () => obj.Product.Data, ProductFactory.Create);
        }

        [TestMethod]
        public async Task PackageTest() {
            var data = GetRandom.ObjectOf<ProductData>();
            data.Id = obj.PackageId;
            data.ProductKind = ProductKind.Package;
            await testItemAsync<ProductData, IProduct, IProductsRepo>(
                data, () => obj.Package.Data, ProductFactory.Create);
        }

    }

}