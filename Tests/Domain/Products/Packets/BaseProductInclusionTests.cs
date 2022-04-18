using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Products.Packets;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Packets {

    [TestClass] public class BaseProductInclusionRuleTests: 
        AbstractTests<BaseProductInclusionRule, Entity<ProductInclusionRuleData>> {
        private class testClass : BaseProductInclusionRule {
            public testClass(ProductInclusionRuleData d = null) : base(d) { }
        }
        protected override BaseProductInclusionRule createObject() => 
            new testClass(GetRandom.ObjectOf<ProductInclusionRuleData>());
        [TestMethod] public void InclusionKindTest() => isReadOnly(obj.Data.InclusionKind);
        [TestMethod] public void MinimumAmountTest() => isReadOnly(obj.Data.MinimumAmount);
        [TestMethod] public void MaximumAmountTest() => isReadOnly(obj.Data.MaximumAmount);
        [TestMethod] public void UnitIdTest() => isReadOnly(obj.Data.UnitId);
        [TestMethod] public async Task UnitTest() =>
            await testItemAsync<UnitData, Unit, IUnitsRepo>(
            obj.UnitId, () => obj.Unit.Data,
            UnitFactory.Create);
        [TestMethod] public void ProductSetIdTest() => isReadOnly(obj.Data.ProductSetId);
        [TestMethod] public void PackageTypeIdTest() => isReadOnly(obj.Data.PackageTypeId);
        [TestMethod] public async Task ProductSetTest() =>
            await testItemAsync<ProductSetData, ProductSet, IProductSetsRepo>(obj.ProductSetId,
                () => obj.ProductSet.Data, d => new ProductSet(d));
        [TestMethod] public async Task MinimumTest() {
            isReadOnly();
            await UnitTest();
            var q = obj.Minimum;
            allPropertiesAreEqual(q.Unit.Data, obj.Unit.Data);
            Assert.AreEqual(q.Amount, obj.MinimumAmount);
        }
        [TestMethod] public async Task MaximumTest() {
            isReadOnly();
            await UnitTest();
            var q = obj.Maximum;
            allPropertiesAreEqual(q.Unit.Data, obj.Unit.Data);
            Assert.AreEqual(q.Amount, obj.MaximumAmount);
        }
        [TestMethod] public void ToStringTest() {
            var setData = GetRandom.ObjectOf<ProductSetData>();
            GetRepo.Instance<IProductSetsRepo>().Add(new(setData));

            var unitData = GetRandom.ObjectOf<UnitData>();
            unitData.Code = rndStr;
            GetRepo.Instance<IUnitsRepo>().Add(UnitFactory.Create(unitData));

            var inclusionRuleData = GetRandom.ObjectOf<ProductInclusionRuleData>();
            inclusionRuleData.ProductSetId = setData.Id;
            inclusionRuleData.UnitId = unitData.Id;
            inclusionRuleData.MinimumAmount = 1;
            inclusionRuleData.MaximumAmount = 2;
            obj = new testClass(inclusionRuleData);

            string expected = $"Select from {inclusionRuleData.MinimumAmount} {unitData.Code} to {inclusionRuleData.MaximumAmount} {unitData.Code} from product set " + "{}";
            string actual = obj.ToString();
            areEqual(expected, actual);
        }
    }
}
