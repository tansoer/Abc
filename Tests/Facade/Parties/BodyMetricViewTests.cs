using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {

    [TestClass] public class BodyMetricViewTests: 
        SealedTests<BodyMetricView, PartyAttributeView> {
        protected override BodyMetricView createObject() 
            => GetRandom.ObjectOf<BodyMetricView>();
        [TestMethod] public void QuantityValueTest() => isProperty<double>("Value");
        [TestMethod] public void StringValueTest() => isNullableProperty<string>("Value");
        [TestMethod] public void ValueTest() => areEqual(obj.ToString(), obj.Value);
        [TestMethod] public void SignatureIdTest() => isNullableProperty<string>("Signed by");
        [TestMethod] public void TypeIdTest() => isNullableProperty<string>("Type");
        [TestMethod] public void UnitIdTest() => isNullableProperty<string>("Unit");
        [TestMethod] public void MetricTypeTest() => isProperty<MetricType>();

        [TestMethod]
        public void ToStringTest() {
            obj.MetricType = MetricType.Quantity;
            areEqual($"{obj.QuantityValue} {obj.UnitId}", obj.ToString());
            obj.MetricType = MetricType.String;
            areEqual($"{obj.StringValue}", obj.ToString());
        }
    }
}
