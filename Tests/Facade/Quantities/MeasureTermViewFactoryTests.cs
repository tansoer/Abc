using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {

    [TestClass]
    public class MeasureTermViewFactoryTests : TestsBase {

        [TestInitialize]
        public virtual void TestInitialize() {
            type = typeof(MeasureTermViewFactory);
        }

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<MeasureTermView>();
            var data = new MeasureTermViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<CommonTermData>();
            var view = new MeasureTermViewFactory().Create(new MeasureTerm(data));

            allPropertiesAreEqual(view, data);
        }

    }

}
