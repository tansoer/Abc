using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Parties;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Orders.Parties {

    [TestClass]
    public class SalesAgentTests :SealedTests<SalesAgent, PartyInOrder> {
        protected override SalesAgent createObject() {
            var d = random<PartySummaryData>();
            d.RoleInOrder = PartyRoleInOrder.SalesAgent;
            return PartyInOrderFactory.Create(d) as SalesAgent;
        }
    }
}