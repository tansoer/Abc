using Abc.Data.Common;

namespace Abc.Data.Rules {
    public sealed class RuleUsageData :EntityBaseData {
        public string RuleId { get; set; }
        public string RuleSetId { get; set; }
    }
}