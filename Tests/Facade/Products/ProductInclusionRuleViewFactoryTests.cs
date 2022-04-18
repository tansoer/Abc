using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class ProductInclusionRuleViewFactoryTests : SealedTests<ProductInclusionRuleViewFactory, AbstractViewFactory<
        ProductInclusionRuleData, IProductInclusionRule, ProductInclusionRuleView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<ProductInclusionRuleView>();
            var data = new ProductInclusionRuleViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<ProductInclusionRuleData>();
            var view = new ProductInclusionRuleViewFactory().Create(ProductInclusionRuleFactory.Create(data));

            allPropertiesAreEqual(view, data);
        }

    }
}
