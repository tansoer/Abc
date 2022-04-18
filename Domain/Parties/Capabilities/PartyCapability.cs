using Abc.Data.Roles;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Roles;
using Abc.Domain.Rules;

namespace Abc.Domain.Parties.Capabilities {
    public sealed class PartyCapability :BasePartyAttribute<PartyCapabilityData> {
        public PartyCapability() :this(null) { }
        public PartyCapability(PartyCapabilityData d) :base(d) { }
        public RuleContext Capability => item<IRuleContextsRepo, RuleContext>(RuleContextId);

        public bool IsCapable(IRuleSet s) {
            var r = s?.Evaluate(Capability);
            var b = r as BooleanVariable;
            return b?.Value ?? false;
        }

        public bool IsCapable(ResponsibilityType r) => IsCapable(r?.Requirements);
        public string RuleContextId => get(Data?.RuleContextId);
    }
}