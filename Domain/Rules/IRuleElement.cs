using Abc.Aids.Enums;
using Abc.Data.Rules;
using Abc.Domain.Common;

namespace Abc.Domain.Rules {

    public interface IRuleElement : IEntity<RuleElementData> {

        RuleElementType Type { get; }

        int Index { get; }

        string RuleId { get; }

        string RuleContextId { get; }

        bool IsRuleElement { get; }

        bool IsContextElement { get; }

        string MasterId { get; }

    }

}