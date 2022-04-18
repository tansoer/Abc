using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Common;

namespace Abc.Facade.Currencies {
    public sealed class PaymentMethodViewFactory :AbstractViewFactory<PaymentMethodData,
        PaymentMethod, PaymentMethodView> {
        protected internal override PaymentMethod toObject(PaymentMethodData d)
            => PaymentMethodFactory.Create(d);
    }
}
