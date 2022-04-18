using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Roles {

    public sealed class PartyRoleView :PartyAttributeView {
        [DisplayName("Role type")] public string PartyRoleTypeId { get; set; }
    }
}
