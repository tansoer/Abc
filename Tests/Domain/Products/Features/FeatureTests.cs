using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Features {

    [TestClass]
    public class FeatureTests : SealedTests<Feature, FeatureValue<FeatureData>> {

        protected override Feature createObject() => new (GetRandom.ObjectOf<FeatureData>());
        [TestMethod] public void productIdTest() => isReadOnly(obj.Data.ProductId);
        [TestMethod] public async Task FeatureTypeTest() 
            => await testItemAsync<FeatureTypeData, FeatureType, IFeatureTypesRepo>(
                obj.featureTypeId, () => obj.FeatureType.Data, d => new FeatureType(d));
        [TestMethod] public void IsValidTest() 
            => areEqual(obj.IsValid(), obj.FeatureType.IsValid(obj.Value));
    }

}