using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Common;

namespace Abc.Pages.Orders {

    public sealed class PaymentsPage :ViewPage<PaymentsPage, IPaymentsRepo, BasePayment, PaymentView, PaymentData> {
        protected override string title => OrderTitles.Payments;
        public PaymentsPage(IPaymentsRepo r) : base(r) { }
        protected internal override string pageUrl => OrderUrls.Payments;
        protected internal override BasePayment toObject(PaymentView v) => new PaymentViewFactory().Create(v);
        protected internal override PaymentView toView(BasePayment o) => new PaymentViewFactory().Create(o);
        protected override void tableColumns() { }
    }
}