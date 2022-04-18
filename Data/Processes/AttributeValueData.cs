using Abc.Aids.Enums;
using Abc.Data.Common;

namespace Abc.Data.Processes {

    public sealed class AttributeValueData :EntityBaseData {
        public string ProcessElementId { get; set; }
        public Relation Equality { get; set; }
        public string AttributeTypeId { get; set; }
        public ValueData Value { get; set; }
        public AttributeValueType Type { get; set; }
    }
}
