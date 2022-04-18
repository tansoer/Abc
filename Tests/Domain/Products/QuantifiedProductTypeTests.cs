using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Quantities;
using Abc.Infra.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products {

    [TestClass] public class QuantifiedProductTypeTests: 
        AbstractTests<QuantifiedProductType, BaseProductType> {
        private class testClass : QuantifiedProductType {
            public testClass(ProductTypeData d) : base(d) { }
        }
        protected override QuantifiedProductType createObject()=> new testClass(GetRandom.ObjectOf<ProductTypeData>());
        [TestMethod] public void UnitIdTest() => isReadOnly(obj.Data.UnitId);
        [TestMethod]public async Task UnitTest() => await testItemAsync<UnitData, Unit, IUnitsRepo>(
            obj.UnitId, () => obj.Unit.Data, UnitFactory.Create);
        [TestMethod] public async Task MeasureTest() {
            await UnitTest();
            await testItemAsync<MeasureData, Measure, IMeasuresRepo>(
                obj.Unit.MeasureId, () => obj.Unit.Measure.Data,
                MeasureFactory.Create);
        }
        [TestMethod] public async Task PossibleUnitsTest() {
            await MeasureTest();
            var units = GetRepo.Instance<IUnitsRepo>() as UnitsRepo;
            removeAll(units?.dbSet, units?.db);
            await testListAsync<Unit, UnitData, IUnitsRepo>(
                d => d.MeasureId = obj.Measure.Id,
                UnitFactory.Create);
        }
        [TestMethod] public void AmountTest() => isReadOnly(obj.Data.Amount);
        [TestMethod] public async Task QuantityOnHandTest() {
            await UnitTest();
            Assert.AreEqual(obj.Amount, obj.QuantityOnHand.Amount);
            Assert.AreEqual(obj.UnitId, obj.QuantityOnHand.Unit.Id);
        }
    }
}