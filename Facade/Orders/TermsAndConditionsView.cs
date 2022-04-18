using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Orders {
    public sealed class TermsAndConditionsView : EntityBaseView {
        [DisplayName("Terms and conditions type")] public string TypeId { get; set; }
        [DisplayName("Order")] public string OrderId { get; set; }
    }
}
