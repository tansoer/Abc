using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Domain.Products.Price;
using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class PriceViewFactoryTests :SealedTests<PriceViewFactory, AbstractViewFactory<
        PriceData, IPrice, PriceView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<PriceView>();
            var data = new PriceViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<PriceData>();
            var view = new PriceViewFactory().Create(PriceFactory.Create(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
