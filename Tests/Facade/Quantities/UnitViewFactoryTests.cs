using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {
    [TestClass] public class UnitViewFactoryTests : TestsBase {
        [TestInitialize] public virtual void TestInitialize() => type = typeof(UnitViewFactory);
        [TestMethod] public void CreateTest() { }
        [TestMethod] public void CreateObjectTest() {
            var view = random<UnitView>();
            var data = new UnitViewFactory().Create(view).Data;
            allPropertiesAreEqual(view, data, nameof(view.Formula));
        }
        [TestMethod]
        public void CreateViewTest() {
            var data = random<UnitData>();
            data.UnitType = random<bool>() ? UnitType.Factored: UnitType.Derived;
            var o = UnitFactory.Create(data);
            var view = new UnitViewFactory().Create(o);
            allPropertiesAreEqual(view, data, nameof(view.Formula));
            areEqual(view.Formula, o.Formula(true));
        }
    }
}
