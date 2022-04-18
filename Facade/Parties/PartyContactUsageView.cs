using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Parties
{
    public sealed class PartyContactUsageView :PartyAttributeView {
        [DisplayName("Contact usage")] public new string Code { get; set; }
        [DisplayName("Comments")] public new string Details { get; set; }
        [DisplayName("Contact")] public new string Name { get; set; }
    }
}