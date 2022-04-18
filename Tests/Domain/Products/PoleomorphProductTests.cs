using System.Linq;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {

    [TestClass] public class PoleomorphProductTests: 
        AbstractTests<PoleomorphProduct<UniqueProductType>, BaseProduct<UniqueProductType>> {
        private class testClass : PoleomorphProduct<UniqueProductType> {
            public testClass(ProductData d) : base(d) { }
            public override UniqueProductType Type => type as UniqueProductType;
        }
        protected override PoleomorphProduct<UniqueProductType> createObject() 
            => new testClass(GetRandom.ObjectOf<ProductData>());
        [TestMethod] public async Task TypeTest() {
            isReadOnly(obj, nameof(obj.Type));
            var d = GetRandom.ObjectOf<ProductTypeData>();
            d.ProductKind = ProductKind.UniqueProduct;
            d.Id = obj.TypeId;
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                d, () => obj.Type.Data, ProductTypeFactory.Create);
        }
        [TestMethod] public async Task FeaturesTest() {
            await TypeTest();
            isReadOnly();

            foreach (var f in obj.Type.Features) {
                foreach (var v in f.PossibleValues) {
                    var x = obj.Features.FirstOrDefault(o => o.Id == v.Id);
                    Assert.IsNotNull(x);
                    allPropertiesAreEqual(x, v, nameof(x.productId));
                }
            }
        }
        [TestMethod] public async Task DetailsTest() {
            await TypeTest();
            isReadOnly(obj.Type.Details);
        }

        [TestMethod] public async Task NameTest() {
            await TypeTest();
            isReadOnly(obj.Type.Name);
        }
        [TestMethod] public async Task CodeTest() {
            await TypeTest();
            isReadOnly(obj.Type.Code);
        }
        [TestMethod] public async Task ValidFromTest() {
            await TypeTest();
            isReadOnly(obj.Type.ValidFrom);
        }
        [TestMethod] public async Task ValidToTest() {
            await TypeTest();
            isReadOnly(obj.Type.ValidTo);
        }
    }
}