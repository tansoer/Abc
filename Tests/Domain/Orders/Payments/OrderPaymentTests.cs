using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Domain.Orders.Payments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Payments {
    [TestClass]
    public class OrderPaymentTests :SealedTests<OrderPayment, BasePayment> {

        protected override OrderPayment createObject() => new (random<PaymentData>());
        [TestMethod] public void ToPaymentMethodIdTest() => isReadOnly(obj.Data.ToPaymentMethodId, true);

        [TestMethod] public async Task ToAccountTest() 
            => await testItemAsync<PaymentMethodData, PaymentMethod, IPaymentMethodsRepo>(
                new PaymentMethodData { 
                    PaymentMethodType = PaymentMethodType.BankAccount,
                    Id = obj.toPaymentMethodId
                }, () => obj.ToAccount.Data, PaymentMethodFactory.Create);
    }
}