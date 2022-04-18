using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Parties;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Orders.Parties {

    [TestClass]
    public class PaymentReceiverTests :SealedTests<PaymentReceiver, PartyInOrder> {
        protected override PaymentReceiver createObject() {
            var d = random<PartySummaryData>();
            d.RoleInOrder = PartyRoleInOrder.PaymentReceiver;
            return PartyInOrderFactory.Create(d) as PaymentReceiver;
        }
    }
}