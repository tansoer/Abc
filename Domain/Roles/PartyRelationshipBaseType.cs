using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Rules;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Domain.Roles {
    public abstract class PartyRelationshipBaseType<TData> :RelationshipType<TData>
        where TData : PartyRelationshipBaseTypeData, new(){
        protected PartyRelationshipBaseType() : this(null) { }
        protected PartyRelationshipBaseType(TData d) : base(d) { }
        public IRuleSet Requirements => item<IRuleSetsRepo, IRuleSet>(ruleSetId);
        public IReadOnlyList<RelationshipConstraint> Constraints
           => list<IRelationshipConstraintsRepo, RelationshipConstraint>(typeId, Id);
        public bool CanFormRelationship(PartyRole consumer, PartyRole provider)
            => Constraints.Any(c => c.CanFormRelationship(consumer, provider));
        public bool CanFormRelationship(PartyRole consumer, PartyRole provider, RuleContext c)
            => CanFormRelationship(consumer, provider) && canFormRelationship(c);
        internal bool canFormRelationship(RuleContext c)
            => (Requirements?.Evaluate(c) as BooleanVariable)?.Value ?? false;
        internal string ruleSetId => get(Data?.RuleSetId);
        internal static string typeId => nameOf<RelationshipConstraintData>(x => x.RelationshipTypeId);
    }
}
