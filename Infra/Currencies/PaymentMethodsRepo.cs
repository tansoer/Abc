using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;

namespace Abc.Infra.Currencies {

    public sealed class PaymentMethodsRepo : PagedRepo<PaymentMethod, PaymentMethodData>,
        IPaymentMethodsRepo {

        public PaymentMethodsRepo(MoneyDb c = null) : base(c, c?.PaymentMethods) { }

        protected internal override PaymentMethod toDomainObject(PaymentMethodData d) => PaymentMethodFactory.Create(d);
    }

}
