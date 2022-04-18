using Abc.Data.Orders;
using Abc.Domain.Orders.Payments;
using Abc.Facade.Orders;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class InvoiceViewFactoryTests :ViewFactoryTests<InvoiceViewFactory,
       InvoiceData, Invoice, InvoiceView> {
    }
}
