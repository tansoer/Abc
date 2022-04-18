using Abc.Data.Currencies;
using Abc.Domain.Common;

namespace Abc.Domain.Currencies {

    public abstract class PaymentMethod : Entity<PaymentMethodData> {
        protected PaymentMethod() : base(null) { }
        protected PaymentMethod(PaymentMethodData d) : base(d) { }
    }
}