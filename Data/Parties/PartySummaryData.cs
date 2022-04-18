
using Abc.Data.Orders;

namespace Abc.Data.Parties {
    public sealed class PartySummaryData : PartyAttributeData {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PartyRoleId { get; set; }
        public string OrderId { get; set; }
        public string OrderLineId { get; set; }
        public string PartySummaryRoleClassifierId { get; set; }
        public PartyRoleInOrder RoleInOrder { get; set; }
        public bool IsPartyRoleInOrder => RoleInOrder != PartyRoleInOrder.Unspecified;
    }
}