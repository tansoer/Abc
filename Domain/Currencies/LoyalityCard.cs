using Abc.Data.Currencies;

namespace Abc.Domain.Currencies {
    public sealed class LoyalityCard :PaymentCard {

        public LoyalityCard(PaymentMethodData d = null) : base(d) { }

    }

}