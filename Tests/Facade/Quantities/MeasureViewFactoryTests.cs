using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {

    [TestClass] public class MeasureViewFactoryTests : TestsBase {
        [TestInitialize] public virtual void TestInitialize() => type = typeof(MeasureViewFactory);
        [TestMethod] public void CreateTest() { }
        [TestMethod] public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<MeasureView>();
            var data = new MeasureViewFactory().Create(view).Data;
            allPropertiesAreEqual(view, data, nameof(view.Formula));
        }
        [TestMethod] public void CreateViewTest() {
            var data = GetRandom.ObjectOf<MeasureData>();
            if (data.MeasureType == MeasureType.Error) data.MeasureType = MeasureType.Base;
            else if (data.MeasureType == MeasureType.Unspecified) data.MeasureType = MeasureType.Derived;
            var o = MeasureFactory.Create(data);
            var view = new MeasureViewFactory().Create(o);
            allPropertiesAreEqual(view, data, nameof(view.Formula));
            areEqual(view.Formula, o.Formula(true));
        }
    }
}
