using Abc.Aids.Enums;
using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass]
    public class BodyMetricDataTests : SealedTests<BodyMetricData, PartyAttributeData> {

        [TestMethod] public void ValueTest() => isProperty<string>();

        [TestMethod] public void TypeIdTest() => isProperty<string>();

        [TestMethod] public void SignatureIdTest() => isProperty<string>();

        [TestMethod] public void UnitIdTest() => isProperty<string>();
        [TestMethod] public void MetricTypeTest() => isProperty<MetricType>();
    }
}