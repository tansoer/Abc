using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Parties;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Orders.Parties {

    [TestClass]
    public class OrderLineReceiverTests :SealedTests<OrderLineReceiver, DeliveryReceiver> {
        protected override OrderLineReceiver createObject() {
            var d = random<PartySummaryData>();
            d.RoleInOrder = PartyRoleInOrder.OrderLineReceiver;
            return PartyInOrderFactory.Create(d) as OrderLineReceiver;
        }
    }
}