using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Parties;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Orders.Parties {

    [TestClass]
    public class VendorTests :SealedTests<Vendor, PartyInOrder> {
        protected override Vendor createObject() {
            var d = random<PartySummaryData>();
            d.RoleInOrder = PartyRoleInOrder.Vendor;
            return PartyInOrderFactory.Create(d) as Vendor;
        }
    }
}