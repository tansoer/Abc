using System.ComponentModel;

namespace Abc.Facade.Common {
    public abstract class ProcessElementBaseView :EntityBaseView {
        [DisplayName("Next element")] public string NextElementId { get; set; }
        [DisplayName("Previous element")] public string PreviousElementId { get; set; }
    }
}
