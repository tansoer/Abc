using Abc.Data.Common;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DateTime = System.DateTime;

namespace Abc.Tests.Data.Currencies {

    [TestClass]
    public class PaymentDataTests : SealedTests<PaymentData, EntityBaseData> {
        [TestMethod] public void AmountTest() => isProperty<decimal>();
        [TestMethod] public void DateDueTest() => isNullable<DateTime?>();
        [TestMethod] public void DateReceivedTest() => isNullable<DateTime?>();
        [TestMethod] public void CurrencyIdTest() => isNullable<string>();
        [TestMethod] public void FromPaymentMethodIdTest() => isNullable<string>();
        [TestMethod] public void ToPaymentMethodIdTest() => isNullable<string>();
        [TestMethod] public void OrderIdTest() => isNullable<string>();
        [TestMethod] public void OrderEventIdTest() => isNullable<string>();
    }
}
