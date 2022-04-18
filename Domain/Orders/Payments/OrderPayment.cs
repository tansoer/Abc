using Abc.Data.Currencies;
using Abc.Domain.Currencies;

namespace Abc.Domain.Orders.Payments {
    public sealed class OrderPayment :BasePayment {
        public OrderPayment() : this(null) { }
        public OrderPayment(PaymentData d) : base(d) { }
        internal string toPaymentMethodId => get(Data?.ToPaymentMethodId);

        public BankAccount ToAccount => item<IPaymentMethodsRepo, PaymentMethod>(toPaymentMethodId) as BankAccount;
    }
}
