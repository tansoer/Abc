using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {

    [TestClass]
    public class BodyMetricTypeViewFactoryTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(BodyMetricTypeViewFactory);

        [TestMethod]
        public void CreateTest() {
            var o = new BodyMetricType(GetRandom.ObjectOf<BodyMetricTypeData>());
            var v = new BodyMetricTypeViewFactory().Create(o);
            allPropertiesAreEqual(v, o.Data);

        }
        [TestMethod]
        public void CreateObjectTest() {
            var v = GetRandom.ObjectOf<BodyMetricTypeView>();
            var o = new BodyMetricTypeViewFactory().Create(v);
            allPropertiesAreEqual(v, o.Data);
        }
    }

}
