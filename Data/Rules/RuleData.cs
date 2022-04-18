using Abc.Data.Common;

namespace Abc.Data.Rules {

    public sealed class RuleData : EntityBaseData {

        public string RuleId { get; set; }

        public RuleKind RuleKind { get; set; }
    }

}