using Abc.Data.Orders;
using Abc.Data.Parties;

namespace Abc.Domain.Orders.Parties {
    public class PartyInOrderFactory {
        public static IPartyInOrder Create(PartySummaryData d) =>
             d?.RoleInOrder switch {
                 PartyRoleInOrder.Vendor => new Vendor(d),
                 PartyRoleInOrder.SalesAgent => new SalesAgent(d),
                 PartyRoleInOrder.PaymentReceiver => new PaymentReceiver(d),
                 PartyRoleInOrder.OrderInitiator => new OrderInitiator(d),
                 PartyRoleInOrder.Purchaser => new Purchaser(d),
                 PartyRoleInOrder.OrderReceiver => new OrderReceiver(d),
                 PartyRoleInOrder.OrderLineReceiver => new OrderLineReceiver(d),
                 _ => new UnspecifiedRoleInOrder(d)
             };
    }
}