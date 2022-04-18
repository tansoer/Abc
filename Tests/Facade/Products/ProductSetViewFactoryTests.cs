using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class ProductSetViewFactoryTests :SealedTests<ProductSetViewFactory, AbstractViewFactory<
        ProductSetData, ProductSet, ProductSetView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<ProductSetView>();
            var data = new ProductSetViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<ProductSetData>();
            var view = new ProductSetViewFactory().Create(new ProductSet(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
