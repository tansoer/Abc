using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;
using Abc.Infra.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Orders {
    [TestClass]
    public class PaymentsRepoTests :OrderReposTests<PaymentsRepo, BasePayment,
        PaymentData> {

        protected override Type getBaseClass() => typeof(PagedRepo<BasePayment, PaymentData>);

        protected override PaymentsRepo getObject(OrderDb c) => new(c);

        protected override DbSet<PaymentData> getSet(OrderDb c) => c.OrderPayments;
    }
}