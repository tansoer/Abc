using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Processes {
    public sealed class AttributeTypeView :EntityBaseView {
        [DisplayName("Element Type")] public string ElementTypeId { get; set; }
        [DisplayName("Is Mandatory")] public bool IsMandatory { get; set; }
    }
}