using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Domain.Orders.Payments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Currencies {
    [TestClass] public class PaymentFactoryTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(PaymentFactory);
        [TestMethod] public void CreateTest() {
            var d = random<PaymentData>();
            var o = PaymentFactory.Create(d);
            allPropertiesAreEqual(d, o.Data);
            isInstanceOfType<OrderPayment>(o);
        }
    }
}