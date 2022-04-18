using Abc.Data.Common;

namespace Abc.Data.Orders {
    public sealed class TermsAndConditionsData: EntityBaseData {
        public string TypeId { get; set; }
        public string OrderId { get; set; }
    }
}
