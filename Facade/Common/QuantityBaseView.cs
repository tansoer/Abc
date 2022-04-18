using System.ComponentModel;

namespace Abc.Facade.Common {
    public abstract class QuantityBaseView :EntityBaseView {
        [DisplayName("Definition")] public new string Details { get; set; }
    }
}
