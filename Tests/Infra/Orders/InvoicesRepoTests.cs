using Abc.Data.Orders;
using Abc.Domain.Orders.Payments;
using Abc.Facade.Orders;
using Abc.Infra.Common;
using Abc.Infra.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Orders {
    [TestClass]
    public class InvoicesRepoTests :OrderReposTests<InvoicesRepo, Invoice,
        InvoiceData> {

        protected override Type getBaseClass() => typeof(PagedRepo<Invoice, InvoiceData>);

        protected override InvoicesRepo getObject(OrderDb c) => new (c);

        protected override DbSet<InvoiceData> getSet(OrderDb c) => c.Invoices;
    }
}