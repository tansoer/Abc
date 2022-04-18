using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Rules;

namespace Abc.Domain.Quantities {

    public sealed class UnitRules : UnitAttribute<UnitRulesData> {
        public UnitRules() : this(null) { }
        public UnitRules(UnitRulesData d) : base(d) { }
        public string FromBaseUnitRuleId => Data?.FromBaseUnitRuleId ?? Unspecified.String;
        public string ToBaseUnitRuleId => Data?.ToBaseUnitRuleId ?? Unspecified.String;
        public BaseRule FromBaseUnitRule => new GetFrom<IRulesRepo, BaseRule>().ById(FromBaseUnitRuleId);
        public BaseRule ToBaseUnitRule => new GetFrom<IRulesRepo, BaseRule>().ById(ToBaseUnitRuleId);
    }
}