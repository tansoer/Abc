using Abc.Data.Common;

namespace Abc.Data.Processes {

    public sealed class AttributeTypeData :EntityBaseData {
        public string ElementTypeId { get; set; }
        public bool IsMandatory { get; set; }
    }
}
