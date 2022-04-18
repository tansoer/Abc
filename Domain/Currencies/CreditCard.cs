using Abc.Data.Currencies;

namespace Abc.Domain.Currencies {

    public sealed class CreditCard : PaymentCard {

        public CreditCard(PaymentMethodData d = null) : base(d) { }

        public Money CreditLimit => creditLimit();

        private Money creditLimit()
            => Data is null ? Money.Unspecified : new Money(Data.CreditLimit, currency);

    }

}