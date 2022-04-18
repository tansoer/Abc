using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class ProductSetContentViewFactoryTests :SealedTests<ProductSetContentViewFactory, AbstractViewFactory<
        ProductSetContentData, ProductSetContent, ProductSetContentView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<ProductSetContentView>();
            var data = new ProductSetContentViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<ProductSetContentData>();
            var view = new ProductSetContentViewFactory().Create(new ProductSetContent(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
