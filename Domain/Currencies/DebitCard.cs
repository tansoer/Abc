using Abc.Data.Currencies;

namespace Abc.Domain.Currencies {

    public sealed class DebitCard : PaymentCard {

        public DebitCard(PaymentMethodData d = null) : base(d) { }

    }

}