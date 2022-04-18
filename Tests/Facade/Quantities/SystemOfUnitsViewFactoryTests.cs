using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {

    [TestClass]
    public class SystemOfUnitsViewFactoryTests : TestsBase {

        [TestInitialize]
        public virtual void TestInitialize() {
            type = typeof(SystemOfUnitsViewFactory);
        }

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<SystemOfUnitsView>();
            var data = new SystemOfUnitsViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<SystemOfUnitsData>();
            var view = new SystemOfUnitsViewFactory().Create(new SystemOfUnits(data));

            allPropertiesAreEqual(view, data);
        }

    }

}
