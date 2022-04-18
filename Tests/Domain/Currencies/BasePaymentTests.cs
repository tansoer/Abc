using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Currencies {
    [TestClass]
    public class BasePaymentTests: AbstractTests<BasePayment, Entity<PaymentData>> {
    private class testClass :BasePayment {
        public testClass(PaymentData d = null) : base(d) { }
    }
    protected override BasePayment createObject() => new testClass(GetRandom.ObjectOf<PaymentData>());
    [TestMethod]
    public async Task MethodTest()
        => await testItemAsync<PaymentMethodData, PaymentMethod, IPaymentMethodsRepo>(
            obj.PaymentMethodId,
            () => obj.Method.Data,
            PaymentMethodFactory.Create);

    [TestMethod]
    public void AmountTest() {
        var d = GetRandom.ObjectOf<CurrencyData>();
        d.Id = obj.CurrencyId;
        GetRepo.Instance<ICurrencyRepo>().Add(new Currency(d));
        var a = isReadOnly(obj, nameof(obj.Amount)) as Money;
        Assert.IsNotNull(a);
        Assert.AreEqual(obj.Data?.Amount, a.Amount);
        Assert.AreEqual(obj.Data?.CurrencyId, a.Currency.Id);
        allPropertiesAreEqual(obj.currency.Data, a.Currency.Data);
    }

    [TestMethod]
    public void DateMadeTest()
        => isReadOnly(obj, nameof(obj.DateMade), obj.Data.ValidFrom);

    [TestMethod]
    public void DateClearedTest()
        => isReadOnly(obj, nameof(obj.DateCleared), obj.Data.ValidTo);

    [TestMethod]
    public void DateReceivedTest()
        => isReadOnly(obj, nameof(obj.DateReceived), obj.Data.DateReceived);

    [TestMethod]
    public void DateDueTest()
        => isReadOnly(obj, nameof(obj.DateDue), obj.Data.DateDue);

    [TestMethod]
    public void CurrencyIdTest()
        => isReadOnly(obj, nameof(obj.CurrencyId), obj.Data.CurrencyId);

    [TestMethod]
    public void PaymentMethodIdTest()
        => isReadOnly(obj, nameof(obj.PaymentMethodId), obj.Data.FromPaymentMethodId);

    [TestMethod]
    public async Task CurrencyTest()
        => await testItemAsync<CurrencyData, Currency, ICurrencyRepo>(
            obj.CurrencyId,
            () => obj.currency.Data,
            d => new Currency(d));
    }
}
