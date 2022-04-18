using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Domain.Orders.Payments;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {
    public sealed class PaymentsRepo :PagedRepo<BasePayment, PaymentData>,
        IPaymentsRepo {

        public PaymentsRepo(OrderDb c = null) : base(c, c?.OrderPayments) { }

        protected internal override BasePayment toDomainObject(PaymentData d) => new OrderPayment(d);
    }
}