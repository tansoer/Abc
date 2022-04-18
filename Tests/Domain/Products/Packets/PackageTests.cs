using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Packets {

    [TestClass]
    public class PackageTests : SealedTests<Package, BaseProduct<PackageType>> {
        [TestMethod]
        public async Task TypeTest() {
            isReadOnly();
            var d = GetRandom.ObjectOf<ProductTypeData>();
            d.ProductKind = ProductKind.Package;
            d.Id = obj.TypeId;
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                d, () => obj.Type.Data, ProductTypeFactory.Create);
        }

        [TestMethod]
        public async Task PackageContentsTest()
            => await testListAsync<PackageContent, PackageContentData, IPackageContentsRepo>(
                obj, nameof(obj.PackageContents),
                x => x.PackageId = obj.Id,
                d => new PackageContent(d));


        [TestMethod]
        public async Task ContentsTest() {
            isReadOnly();
            Assert.AreEqual(0, obj.Contents.Count);
            await PackageContentsTest();

            foreach (var e in obj.PackageContents) {
                var d = GetRandom.ObjectOf<ProductData>();
                d.Id = e.ProductId;
                await addAsync<IProductsRepo, IProduct>(ProductFactory.Create(d));
            }

            Assert.AreNotEqual(0, obj.Contents.Count);
            Assert.AreEqual(obj.PackageContents.Count, obj.Contents.Count);
        }

    }

}