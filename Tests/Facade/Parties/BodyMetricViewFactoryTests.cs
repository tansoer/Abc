using System;
using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {

    [TestClass]
    public class BodyMetricViewFactoryTests :TestsBase {

        [TestInitialize]
        public void TestInitalize() => type = typeof(BodyMetricViewFactory);

        [TestMethod]
        public void CreateTest() {
            foreach (var t in (MetricType[])Enum.GetValues(typeof(MetricType))) {
                var view = GetRandom.ObjectOf<BodyMetricView>();
                view.MetricType = t;
                var o = new BodyMetricViewFactory().Create(view);
                view = new BodyMetricViewFactory().Create(o);
                allPropertiesAreEqual(o.Data, view, "Value");
            }
        }

    }
}
