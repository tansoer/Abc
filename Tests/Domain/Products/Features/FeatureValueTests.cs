using Abc.Data.Common;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products.Features;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Features {
    [TestClass] public class FeatureValueTests:
        AbstractTests<FeatureValue<FeatureData>, Entity<FeatureData>> {
        FeatureSpecData specData;
        private class testClass :FeatureValue<FeatureData> {
            public testClass(FeatureData d = null) : base(d) { }
        }
        protected override FeatureValue<FeatureData> createObject() => new testClass(random<FeatureData>());
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            specData = randomSpecData;
        }
        [TestMethod] public void featureTypeIdTest() => isReadOnly(obj.Data.FeatureTypeId);
        [TestMethod] public void valueSpecIdTest() => isReadOnly(obj.Data.ValueSpecId);
        [TestMethod] public async Task FeatureTypeTest()
           => await testItemAsync<FeatureTypeData, FeatureType, IFeatureTypesRepo>(
               obj.featureTypeId, () => obj.FeatureType.Data, d => new FeatureType(d));
        [TestMethod] public async Task ValueSpecTest()
           => await testItemAsync<FeatureSpecData, FeatureSpec, IFeatureSpecsRepo>(
               specData, () => obj.ValueSpec.Data, d => new FeatureSpec(d));
        [TestMethod] public async Task ValueTest() {
            await ValueSpecTest();
            allPropertiesAreEqual(specData.Value, obj.ValueSpec.Data.Value);
        }
        public FeatureSpecData randomSpecData { 
            get { 
                var d = random<FeatureSpecData>();
                d.Id = obj.valueSpecId;
                d.Value = random<ValueData>();
                d.Value.ValueType = ValueType.String;
                d.Value.UnitOrCurrencyId = null;
                return d;
            } 
        }
    }
}