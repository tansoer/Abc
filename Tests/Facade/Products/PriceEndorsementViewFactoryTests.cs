using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products.Price;
using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class PriceEndorsementViewFactoryTests :SealedTests<PriceEndorsementViewFactory, AbstractViewFactory<
        PriceEndorsementData, PriceEndorsement, PriceEndorsementView>> {
        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<PriceEndorsementView>();
            var data = new PriceEndorsementViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<PriceEndorsementData>();
            var view = new PriceEndorsementViewFactory().Create(new PriceEndorsement(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
