using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {

    [TestClass] public class UnitTermViewFactoryTests : TestsBase {
        [TestInitialize] public virtual void TestInitialize() => type = typeof(UnitTermViewFactory);
        [TestMethod] public void CreateTest() { }
        [TestMethod] public void CreateObjectTest() {
            var view = random<UnitTermView>();
            var data = new UnitTermViewFactory().Create(view).Data;
            allPropertiesAreEqual(view, data);
        }
        [TestMethod] public void CreateViewTest() {
            var data = random<CommonTermData>();
            var view = new UnitTermViewFactory().Create(new UnitTerm(data));
            allPropertiesAreEqual(view, data);
        }
    }
}