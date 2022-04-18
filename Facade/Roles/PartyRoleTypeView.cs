using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Roles
{
    public sealed class PartyRoleTypeView : EntityTypeView {
        [DisplayName("Rule set")]public string RuleSetId { get; set; }
    }
}