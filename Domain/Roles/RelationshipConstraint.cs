using Abc.Data.Roles;
using Abc.Domain.Common;

namespace Abc.Domain.Roles {
    public sealed class RelationshipConstraint: Entity<RelationshipConstraintData> {
        public RelationshipConstraint() : this(null) { }
        public RelationshipConstraint(RelationshipConstraintData d) : base(d) { }
        public RelationshipConstraintType Type
            => item<IRelationshipConstraintTypesRepo, RelationshipConstraintType>(constraintTypeId);
        public bool CanFormRelationship(PartyRole consumer, PartyRole provider)
            => Type.CanFormRelationship(consumer, provider);
        internal string constraintTypeId => get(Data?.ConstraintTypeId);
    }
}
