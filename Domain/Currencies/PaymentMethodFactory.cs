using Abc.Data.Currencies;

namespace Abc.Domain.Currencies {

    public static class PaymentMethodFactory {

        public static PaymentMethod Create(PaymentMethodData d = null)
            => d?.PaymentMethodType switch {
                PaymentMethodType.Check => new Check(d),
                PaymentMethodType.CreditCard => new CreditCard(d),
                PaymentMethodType.DebitCard => new DebitCard(d),
                PaymentMethodType.BankAccount => new BankAccount(d),
                PaymentMethodType.LoyaltyCard => new LoyalityCard(d),
                _ => new Cash(d)
            };
    }
}
