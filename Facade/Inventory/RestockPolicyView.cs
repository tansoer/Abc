using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Inventory {
    public sealed class RestockPolicyView :EntityBaseView {
        [DisplayName("Restock Rule Set")]
        public string RestockRuleSetId { get; set; }
        [DisplayName("Restock Rule Context")]
        public string RestockRuleContextId { get; set; }
    }
}
