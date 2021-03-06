using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Parties;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Orders.Parties {

    [TestClass]
    public class OrderReceiverTests :SealedTests<OrderReceiver, DeliveryReceiver> {
        protected override OrderReceiver createObject() {
            var d = random<PartySummaryData>();
            d.RoleInOrder = PartyRoleInOrder.OrderReceiver;
            return PartyInOrderFactory.Create(d) as OrderReceiver;
        }
    }
}