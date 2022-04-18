using Abc.Aids.Enums;
using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public static class RuleElementFactory {

        public static RuleElementData Create(RuleElement e) => e.Data;

        public static IRuleElement Create(RuleElementData d) {
            if (d is null) return VariableFactory.Create(null);
            return d.RuleElementType switch
            {
                RuleElementType.Operator => new Operator(d),
                RuleElementType.Operand => new Operand(d),
                _ => VariableFactory.Create(d)
            };
        }

    }

}