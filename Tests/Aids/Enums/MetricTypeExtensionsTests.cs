using Abc.Aids.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Enums {
    [TestClass]
    public class MetricTypeExtensionsTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(MetricTypeExtensions);

        [TestMethod]
        public void IsStringTest() {
            isTrue(MetricType.String.IsString());
            isFalse(MetricType.Quantity.IsString());
            isFalse(MetricType.Unspecified.IsString());
        }

        [TestMethod]
        public void IsQuantityTest() {
            isTrue(MetricType.Quantity.IsQuantity());
            isFalse(MetricType.String.IsQuantity());
            isFalse(MetricType.Unspecified.IsQuantity());
        }
    }
}
