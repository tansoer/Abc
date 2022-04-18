using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Attributes {

    [TestClass]
    public class BodyMetricStringTests
        : SealedTests<BodyMetricString, BodyMetric<string>> {

        protected override BodyMetricString createObject() => new BodyMetricString(GetRandom.ObjectOf<BodyMetricData>());

        [TestMethod] public void ValueTest() => isReadOnly(obj.Data.Value);

        [TestMethod] public void ToStringTest() => areEqual(obj.Value, obj.ToString());
    }

}