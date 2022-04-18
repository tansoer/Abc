using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Domain.Orders.Payments;
using Abc.Facade.Currencies;
using Abc.Facade.Orders;
using Abc.Pages.Common;
using Abc.Pages.Orders;
using Abc.Tests.Facade.Orders;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Orders {
    [TestClass]
    public class PaymentsPageTests : SealedViewPageTests<
        PaymentsPage,
        IPaymentsRepo,
        BasePayment,
        PaymentView,
        PaymentData> {

        protected override System.Type getBaseClass() =>
            typeof(ViewPage<PaymentsPage, IPaymentsRepo,
        BasePayment,
        PaymentView,
        PaymentData>);
        protected override string pageTitle => OrderTitles.Payments;
        protected override string pageUrl => OrderUrls.Payments;
        protected override BasePayment toObject(PaymentData d) => new OrderPayment(d);
        private class testRepo : mockRepo<BasePayment, PaymentData>, IPaymentsRepo { }

        private testRepo Repo;

        protected override PaymentsPage createObject() {
            Repo = new testRepo();
            addRandomCountries();
            return new PaymentsPage(Repo);
        }
        private void addRandomCountries() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                Repo.Add(new OrderPayment(GetRandom.ObjectOf<PaymentData>()));
        }

    }
}
