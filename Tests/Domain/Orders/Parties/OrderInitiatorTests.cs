using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Parties;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Orders.Parties {

    [TestClass]
    public class OrderInitiatorTests :SealedTests<OrderInitiator, PartyInOrder> {
        protected override OrderInitiator createObject() {
            var d = random<PartySummaryData>();
            d.RoleInOrder = PartyRoleInOrder.OrderInitiator;
            return PartyInOrderFactory.Create(d) as OrderInitiator;
        }
    }
}