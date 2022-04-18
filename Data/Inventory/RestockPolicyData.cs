using Abc.Data.Common;

namespace Abc.Data.Inventory {
    public sealed class RestockPolicyData : EntityBaseData{
        public string RestockRuleSetId { get; set; }
        public string RestockRuleContextId { get; set; }
    }
}