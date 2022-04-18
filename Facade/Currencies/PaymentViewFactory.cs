using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Common;

namespace Abc.Facade.Currencies
{
    public sealed class PaymentViewFactory : AbstractViewFactory<PaymentData, BasePayment, PaymentView>
    {
        protected internal override BasePayment toObject(PaymentData d) => PaymentFactory.Create(d);
    }
}