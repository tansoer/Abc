using Abc.Aids.Enums;
using Abc.Data.Rules;
using Abc.Domain.Common;

namespace Abc.Domain.Rules {
    public abstract class RuleElement : Entity<RuleElementData>, IRuleElement {
        protected RuleElement() : this(null) { }
        protected RuleElement(RuleElementData d) : base(d) { }
        public RuleElementType Type => Data?.RuleElementType ?? RuleElementType.Unspecified;
        public int Index => Data?.Index ?? Unspecified.Integer;
        public string RuleId => Data?.RuleId ?? Unspecified.String;
        public string RuleContextId => Data?.RuleContextId ?? Unspecified.String;
        public bool IsRuleElement => RuleId != Unspecified.String;
        public bool IsContextElement => RuleContextId != Unspecified.String;
        public string MasterId => IsRuleElement ? RuleId : RuleContextId;
    }
}