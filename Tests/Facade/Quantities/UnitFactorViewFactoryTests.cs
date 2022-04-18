using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {

    [TestClass]
    public class UnitFactorViewFactoryTests : TestsBase {

        [TestInitialize] public virtual void TestInitialize() => type = typeof(UnitFactorViewFactory);

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<UnitFactorView>();
            var data = new UnitFactorViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<UnitFactorData>();
            var view = new UnitFactorViewFactory().Create(new UnitFactor(data));

            allPropertiesAreEqual(view, data);
        }

    }

}
