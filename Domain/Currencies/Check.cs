
using System;
using Abc.Data.Currencies;

namespace Abc.Domain.Currencies {

    public sealed class Check : BaseBankAccount {

        public Check() : this(null) { }
        public Check(PaymentMethodData d) : base(d) { }

        public string CheckNumber => get(Data?.CardOrCheckNumber);
        public string Payee => get(Data?.Payee);
        public DateTime DateWritten => get(Data?.ValidFrom);

    }
}