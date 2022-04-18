using Abc.Data.Currencies;

namespace Abc.Domain.Currencies {

    public sealed class Cash : PaymentMethod {
        public Cash(PaymentMethodData d = null) : base(d) { }
    }

}