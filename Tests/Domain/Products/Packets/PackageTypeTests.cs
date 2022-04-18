using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Packets {

    [TestClass] public class PackageTypeTests :SealedTests<PackageType, BaseProductType> {

        protected override PackageType createObject() {
            var d = GetRandom.ObjectOf<ProductTypeData>();
            d.ProductKind = ProductKind.Package;

            return ProductTypeFactory.Create(d) as PackageType;
        }

        [TestMethod] public async Task PackageComponentsTest()
            => await testListAsync<PackageComponent, PackageComponentData, IPackageComponentsRepo>(
                x => x.PackageTypeId = obj.Id,
                d => new PackageComponent(d));

        [TestMethod] public async Task InclusionRulesTest()
            => await testListAsync<IProductInclusionRule, ProductInclusionRuleData, IProductInclusionRulesRepo>(
                x => x.PackageTypeId = obj.Id,
                ProductInclusionRuleFactory.Create);

        [TestMethod] public async Task RelatedSetsTest()
            => await testListAsync<ProductSetContent, ProductSetContentData, IProductSetContentsRepo>(
                x => x.ProductTypeId = obj.Id,
                d => new ProductSetContent(d));

        [TestMethod] public void ProductSetsTest() {
            testRelatedList<ProductSet, ProductSetData, ProductSetContent, IProductSetsRepo>(
                () => obj.ProductSets,
                () => obj.RelatedSets,
                (d, e) => {
                    d.Id = e.ProductSetId;
                    return new ProductSet(d);
                }, RelatedSetsTest, (o, e) => o.Id == e.ProductSetId );
        }

        [TestMethod] public void ComponentsTest() {
            testRelatedList<IProductType, ProductTypeData, PackageComponent, IProductTypesRepo>(
                () => obj.Components,
                () => obj.PackageComponents,
                (d, e) => {
                    d.Id = e.ProductTypeId;
                    if (d.ProductKind == ProductKind.Unspecified)
                        d.ProductKind = ProductKind.Product;
                    return ProductTypeFactory.Create(d);
                }, PackageComponentsTest, (o,e) => o.Id== e.ProductTypeId );
        }

        [DataTestMethod] [DataRow("Menu")] [DataRow("Computer")] public void ValidateTest(string testName) {
            switch (testName) {
                case "Menu":
                    new PackageLunchSpecificationTests().Run(obj);

                    break;
                case "Computer":
                    new PackageComputerSpecificationTest().Run(obj);

                    break;
                default:
                    Assert.Fail();

                    break;
            }
        }

    }

}