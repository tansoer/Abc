using Abc.Data.Currencies;
using Abc.Domain.Orders.Payments;

namespace Abc.Domain.Currencies {
    public static class PaymentFactory {
        public static OrderPayment Create(PaymentData d = null) => new (d);
    }
}
