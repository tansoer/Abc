using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;

namespace Abc.Domain.Rules {
    public sealed class RuleOverride : BasePartySignature<RuleOverrideData> {
        public RuleOverride() : this(null) { }
        public RuleOverride(RuleOverrideData d) : base(d) { }
        public string RuleSetId => Data?.RuleSetId ?? Unspecified.String;
        public string RuleId => Data?.RuleId ?? Unspecified.String;
        public string RuleContextId => Data?.RuleContextId ?? Unspecified.String;
        public string VariableId => Data?.VariableId ?? Unspecified.String;
        public IRuleSet RuleSet => new GetFrom<IRuleSetsRepo, IRuleSet>().ById(RuleSetId) ?? new RuleSet();
        public BaseRule Rule => new GetFrom<IRulesRepo, BaseRule>().ById(RuleId) ?? new UnspecifiedRule();
        public RuleContext RuleContext => new GetFrom<IRuleContextsRepo, RuleContext>().ById(RuleContextId) ??
                                          new RuleContext();
        public IVariable RuleVariable =>
            new GetFrom<IRuleElementsRepo, IRuleElement>().ById(VariableId) as IVariable ??
            new UnspecifiedVariable();
    }
}