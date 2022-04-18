using System;
using Abc.Data.Currencies;
using Abc.Domain.Common;

namespace Abc.Domain.Currencies {
    public abstract class BasePayment:Entity<PaymentData> {
        protected BasePayment() : this(null) { }
        protected BasePayment(PaymentData d) : base(d) { }
        public PaymentMethod Method => item<IPaymentMethodsRepo, PaymentMethod>(PaymentMethodId);
        public Money Amount => getAmount();
        public DateTime DateMade => get(Data?.ValidFrom);
        public DateTime DateReceived => get(Data?.DateReceived);
        public DateTime DateDue => get(Data?.DateDue, false);
        public DateTime DateCleared => get(Data?.ValidTo, false);
        public string CurrencyId => get(Data?.CurrencyId);
        public string PaymentMethodId => get(Data?.FromPaymentMethodId);
        private Money getAmount()
            => Data is null ? Money.Unspecified : new Money(Data.Amount, currency);
        internal Currency currency => new GetFrom<ICurrencyRepo, Currency>().ById(CurrencyId);
    }
}