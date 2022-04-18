using System;
using Abc.Data.Currencies;
using Abc.Domain.Common;

namespace Abc.Domain.Currencies {
    public abstract class PaymentCard : PaymentMethod {

        protected PaymentCard(PaymentMethodData d = null) : base(d) { }
        public CardAssociation CardAssociation => new GetFrom<ICardAssociationsRepo, CardAssociation>().ById(CardAssociationId);
        protected internal Currency currency =>
            new GetFrom<ICurrencyRepo, Currency>().ById(Data?.CurrencyId ?? Unspecified.String);
        public Money DailyLimit => dailyLimit();
        public string CardNumber => Data?.CardOrCheckNumber ?? Unspecified.String;
        public string CardAssociationId => Data?.CardAssociationId ?? Unspecified.String;
        public string CurrencyId => Data?.CurrencyId ?? Unspecified.String;
        public string NameOnCard => Data?.Name ?? Unspecified.String;
        public DateTime ExpiringDate => Data?.ValidTo ?? DateTime.MinValue;
        public string BillingAddress => Data?.BillingOrBankAddress ?? Unspecified.String;
        public override DateTime ValidFrom => Data?.ValidFrom ?? DateTime.MaxValue;
        public string VerificationCode => Data?.Code ?? Unspecified.String;
        public string IssueNumber => Data?.IssueOrBankIdNumber ?? Unspecified.String;
        public override DateTime ValidTo => Data?.ValidTo ?? DateTime.MinValue;
        private Money dailyLimit()
            => Data is null ? Money.Unspecified : new Money(Data.DailyLimit, currency);
    }
}