using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {

    [TestClass]
    public class MeasureFactoryTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(MeasureFactory);

        [TestMethod]
        public void CreateTest() =>
            Assert.IsInstanceOfType(MeasureFactory.Create(), typeof(BaseMeasure));

        [TestMethod]
        public void CreateBaseTest() =>
            Assert.IsInstanceOfType(MeasureFactory.Create(
                new MeasureData { MeasureType = MeasureType.Base }), typeof(BaseMeasure));

        [TestMethod]
        public void CreateDerivedTest() =>
            Assert.IsInstanceOfType(MeasureFactory.Create(
                new MeasureData { MeasureType = MeasureType.Derived }), typeof(DerivedMeasure));

    }

}
