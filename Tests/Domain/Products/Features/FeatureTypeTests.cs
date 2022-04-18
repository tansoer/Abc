using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Features;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Features {
    [TestClass] public class FeatureTypeTests :SealedTests<FeatureType, Entity<FeatureTypeData>> {
        private bool allTrue = false;
        private bool anyTrue = false;
        private bool allFalse = false;
        protected override FeatureType createObject() => new(GetRandom.ObjectOf<FeatureTypeData>());
        protected static FeatureType createObject(string id, bool mustSatisfyAll) {
            var d = GetRandom.ObjectOf<FeatureTypeData>();
            d.Id = id;
            d.MustSatisfyAll = mustSatisfyAll;
            return new FeatureType(d);
        }
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            allTrue = false;
            anyTrue = false;
            allFalse = false;
        }
        [TestMethod] public void productTypeIdTest() => isReadOnly(obj.Data.ProductTypeId);
        [TestMethod] public async Task ProductTypeTest()
           => await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
               obj.productTypeId, () => obj.ProductType.Data, d => ProductTypeFactory.Create(d));
        [TestMethod] public void IsMandatoryTest() => isReadOnly(obj.Data.IsMandatory);
        [TestMethod] public void MustSatisfyAllTest() => isReadOnly(obj.Data.MustSatisfyAll);
        [TestMethod] public void ValueTypeTest() => isReadOnly(obj.Data.ValueType);
        [TestMethod] public void NumericCodeTest() => isReadOnly(obj.Data.NumericCode);
        [TestMethod] public async Task PossibleValuesTest() {
            await testListAsync<PossibleFeatureValue, PossibleFeatureValueData, IPossibleFeatureValuesRepo>(
                obj, nameof(obj.PossibleValues),
                x => x.FeatureTypeId = obj.Id,
                d => createPossibleFeatureValue(d));
        }
        [DataRow(false)]
        [DataRow(true)]
        [TestMethod] public async Task IsValidAllTest(bool expected) {
            obj = createObject();
            obj.data.MustSatisfyAll = true;
            allFalse = !expected;
            allTrue = expected;
            await PossibleValuesTest();
            areEqual(expected, obj.IsValid(rndFeature));
        }
        [DataRow(false)]
        [DataRow(true)]
        [TestMethod] public async Task IsValidAnyTest(bool expected) {
            obj = createObject();
            obj.data.MustSatisfyAll = false;
            allFalse = !expected;
            anyTrue = expected;
            await PossibleValuesTest();
            areEqual(expected, obj.IsValid(rndFeature));
        }
        [TestMethod] public void IsValidTest() {}
        private PossibleFeatureValue createPossibleFeatureValue(PossibleFeatureValueData d) {
            if (allTrue) return allTrueFeatureValues(d);
            if (anyTrue) return anyTrueFeatureValues(d);
            if (allFalse) return allFalseFeatureValues(d);
            return new PossibleFeatureValue(d);
        }
        private PossibleFeatureValue allFalseFeatureValues(PossibleFeatureValueData d) {
            d.Relation = Relation.IsEqual;
            d.ValueSpecId = rndSpecId;
            return new PossibleFeatureValue(d);
        }
        private PossibleFeatureValue anyTrueFeatureValues(PossibleFeatureValueData d) {
            d.Relation = (!allFalse)? Relation.IsNotEqual: Relation.IsEqual;
            allFalse = !allFalse;
            d.ValueSpecId = rndSpecId;
            return new PossibleFeatureValue(d);
        }
        private static PossibleFeatureValue allTrueFeatureValues(PossibleFeatureValueData d) {
            d.Relation = Relation.IsNotEqual;
            d.ValueSpecId = rndSpecId;
            return new PossibleFeatureValue(d);
        }
        private static string rndSpecId { get {
                var v = random<FeatureSpecData>();
                v.Value = random<ValueData>();
                v.Value.UnitOrCurrencyId = null;
                v.Value.ValueType = ValueType.String;
                getRepo<IFeatureSpecsRepo>().Add(new FeatureSpec(v));
                return v.Id;
            }
        }
        public static Feature rndFeature {
            get {
                var d = random<FeatureData>();
                d.ValueSpecId = rndSpecId;
                return new Feature(d);
            }
        }
    }
}