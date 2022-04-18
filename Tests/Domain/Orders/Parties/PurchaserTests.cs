using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Parties;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Orders.Parties {

    [TestClass]
    public class PurchaserTests :SealedTests<Purchaser, PartyInOrder> {
        protected override Purchaser createObject() {
            var d = random<PartySummaryData>();
            d.RoleInOrder = PartyRoleInOrder.Purchaser;
            return PartyInOrderFactory.Create(d) as Purchaser;
        }
    }
}