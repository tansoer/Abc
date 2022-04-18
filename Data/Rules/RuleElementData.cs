using Abc.Aids.Calculator;
using Abc.Aids.Enums;
using Abc.Data.Common;

namespace Abc.Data.Rules {

    public sealed class RuleElementData : EntityBaseData {

        public int Index { get; set; }

        public string RuleId { get; set; }

        public string RuleContextId { get; set; }

        public string ActivityId { get; set; }

        public string UnitOrCurrencyId { get; set; }

        public string Value { get; set; }

        public RuleElementType RuleElementType { get; set; }

        public Operation Operation { get; set; }

    }

}

