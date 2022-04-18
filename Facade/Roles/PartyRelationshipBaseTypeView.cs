using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Roles {
    public abstract class PartyRelationshipBaseTypeView :RelationshipTypeView {
        [DisplayName("Rule set")] public string RuleSetId { get; set; }
    }
}
