using Abc.Domain.Common;

namespace Abc.Domain.Roles {
    public interface IPartyRolesRepo: IRepo<PartyRole> { }
    public interface IPartyRoleTypesRepo: IRepo<PartyRoleType> {}
    public interface IPartyRelationshipsRepo :IRepo<PartyRelationship> { }
    public interface IPartyRelationshipTypesRepo :IRepo<PartyRelationshipType> { }
    public interface IPartyRoleConstraintsRepo :IRepo<PartyRoleConstraint> { }
    public interface IPartyRoleConstraintTypesRepo :IRepo<PartyRoleConstraintType> { }
    public interface IRelationshipConstraintTypesRepo :IRepo<RelationshipConstraintType> { }
    public interface IRelationshipConstraintsRepo :IRepo<RelationshipConstraint> { }
    public interface IResponsibilitiesRepo :IRepo<Responsibility> { }
    public interface IResponsibilityTypesRepo :IRepo<ResponsibilityType> { }
    public interface IAssignedResponsibilitiesRepo :IRepo<AssignedResponsibility> { }
}