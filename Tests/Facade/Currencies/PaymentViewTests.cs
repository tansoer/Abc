using Abc.Facade.Common;
using Abc.Facade.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Facade.Currencies {
    [TestClass]
    public class PaymentViewTests :SealedTests<PaymentView, EntityBaseView> {
        [TestMethod] public void AmountTest() => isProperty<decimal>("Amount");
        [TestMethod] public void CurrencyIdTest() => isNullableProperty<string>("Currency");
        [TestMethod] public void FromPaymentMethodIdTest() => isNullableProperty<string>("From payment method");
        [TestMethod] public void DateReceivedTest() => isNullableProperty<DateTime?>("Date received");
        [TestMethod] public void DateDueTest() => isNullableProperty<DateTime?>("Date due");
        [TestMethod] public void OrderIdTest() => isNullableProperty<string>("Order");
        [TestMethod] public void OrderEventIdTest() => isNullableProperty<string>("Order event");
        [TestMethod] public void ToPaymentMethodIdTest() => isNullableProperty<string>("To payment method");
    }
}
