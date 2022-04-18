using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Data.Quantities;
using Abc.Domain.Products;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {

    [TestClass]
    public class MeasuredProductTests : SealedTests<MeasuredProduct, BaseProduct<MeasuredProductType>> {

        [TestMethod]
        public async Task TypeTest() {
            isReadOnly();
            var d = GetRandom.ObjectOf<ProductTypeData>();
            d.ProductKind = ProductKind.MeasuredProduct;
            d.Id = obj.TypeId;
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                d, () => obj.Type.Data, ProductTypeFactory.Create);
        }

        [TestMethod] public void UnitIdTest() => isReadOnly(obj.Data.UnitId);

        [TestMethod]
        public async Task UnitTest() => await testItemAsync<UnitData, Unit, IUnitsRepo>(
            obj.UnitId, () => obj.Unit.Data, UnitFactory.Create);

        [TestMethod] public void AmountTest() => isReadOnly(obj.Data.Amount);

        [TestMethod]
        public async Task QuantityTest() {
            await UnitTest();
            Assert.AreEqual(obj.Amount, obj.Quantity.Amount);
            Assert.AreEqual(obj.UnitId, obj.Quantity.Unit.Id);
        }

    }

}