using Abc.Data.Orders;
using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Parties {
    public sealed class PartySummaryView :PartyAttributeView {
        public string Address { get; set; }
        [DisplayName("Phone number")] public string PhoneNumber { get; set; }
        [DisplayName("Email address")] public string EmailAddress { get; set; }
        [DisplayName("Party role")] public string PartyRoleId { get; set; }
        [DisplayName("Order")] public string OrderId { get; set; }
        [DisplayName("Order line")] public string OrderLineId { get; set; }
        [DisplayName("Other role")] public string PartySummaryRoleClassifierId { get; set; }
        [DisplayName("Role in order")] public PartyRoleInOrder RoleInOrder { get; set; }
        public bool IsPartyRoleInOrder => RoleInOrder != PartyRoleInOrder.Unspecified;
    }
}