using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Roles;

namespace Abc.Domain.Orders.Parties {

    public interface IPartyInOrder :IPartySummary {
        IOrder Order { get; }
        OrderLine OrderLine { get; }
        PartyRoleInOrder RoleInOrder { get; }
        bool IsRoleTypeOf(PartyRoleInOrder t);
        bool IsCorrect();
    }

    public abstract class PartyInOrder :BasePartySummary, IPartyInOrder {
        protected PartyInOrder() :this(null) { }
        protected PartyInOrder(PartySummaryData d) :base(d) { }
        internal string orderId => get(Data?.OrderId);
        internal string orderLineId => get(Data?.OrderLineId);
        
        public IOrder Order => item<IOrdersRepo, IOrder>(orderId);
        public OrderLine OrderLine => item<IOrderLinesRepo, IOrderLine>(orderLineId) as OrderLine;
        
        public PartyRoleInOrder RoleInOrder
            => Data?.RoleInOrder ?? PartyRoleInOrder.Unspecified;
        
        public bool IsRoleTypeOf(PartyRoleInOrder p) => p switch {
            PartyRoleInOrder.Unspecified => (this is UnspecifiedRoleInOrder) || Data.RoleInOrder == p,
            PartyRoleInOrder.Vendor => (this is Vendor) || Data.RoleInOrder == p,
            PartyRoleInOrder.SalesAgent => (this is SalesAgent) || Data.RoleInOrder == p,
            PartyRoleInOrder.PaymentReceiver => (this is PaymentReceiver) || Data.RoleInOrder == p,
            PartyRoleInOrder.OrderInitiator => (this is OrderInitiator) || Data.RoleInOrder == p,
            PartyRoleInOrder.Purchaser => (this is Purchaser) || Data.RoleInOrder == p,
            PartyRoleInOrder.OrderReceiver => (this is OrderReceiver) || Data.RoleInOrder == p,
            PartyRoleInOrder.OrderLineReceiver => (this is OrderLineReceiver)
                                                         || Data.RoleInOrder == p,
            _ => false
        };
        public bool IsCorrect() => !IsUnspecified;
 
        protected internal override bool isUnspecified() =>
            arePropertiesEqual(data, new PartySummaryData(),
                nameof(Data.Id), nameof(Data.ValidFrom), nameof(Data.ValidTo), 
                nameof(Data.OrderId), nameof(Data.OrderLineId));
    }
}