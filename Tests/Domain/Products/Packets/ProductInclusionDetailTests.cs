using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Packets {

    [TestClass]
    public class
        ProductInclusionRuleDetailTests : SealedTests<ProductInclusionRuleDetail, BaseProductInclusionRule> {

        [TestMethod]
        public void MasterInclusionIdTest() => isReadOnly(obj.Data.ProductInclusionId);
        [TestMethod]
        public async Task MasterInclusionTest() {
            isReadOnly();
            var d = GetRandom.ObjectOf<ProductInclusionRuleData>();
            d.InclusionKind = ProductInclusionKind.Conditional;
            d.Id = obj.MasterInclusionId;
            await testItemAsync<ProductInclusionRuleData, IProductInclusionRule, IProductInclusionRulesRepo>(
                d, () => obj.MasterInclusion.Data, ProductInclusionRuleFactory.Create);
        }


    }

}