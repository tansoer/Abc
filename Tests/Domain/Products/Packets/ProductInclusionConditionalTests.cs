using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Packets {

    [TestClass] public class
        ProductInclusionRuleConditionTests :SealedTests<ProductInclusionRuleCondition, BaseProductInclusionRule> {

        [TestMethod] public async Task ProductInclusionDetailsTest()
            => await testListAsync<IProductInclusionRule, ProductInclusionRuleData, IProductInclusionRulesRepo>(
                obj, nameof(obj.ProductInclusionDetails),
                x => {
                    x.ProductInclusionId = obj.Id;
                    x.InclusionKind = ProductInclusionKind.Detail;
                },
                ProductInclusionRuleFactory.Create);

        [TestMethod] public async Task DetailedInclusionsTest() {
            isReadOnly();
            Assert.AreEqual(0, obj.DetailedInclusions.Count);
            await ProductInclusionDetailsTest();
            Assert.AreNotEqual(0, obj.DetailedInclusions.Count);
            Assert.AreEqual(obj.ProductInclusionDetails.Count, obj.DetailedInclusions.Count);
        }

    }

}