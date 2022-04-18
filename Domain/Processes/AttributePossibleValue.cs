using Abc.Aids.Enums;
using Abc.Data.Processes;

namespace Abc.Domain.Processes {
    public sealed class AttributePossibleValue :AttributeValue {
        public AttributePossibleValue() : this(null) { }
        public AttributePossibleValue(AttributeValueData d) : base(d) { }
        public Relation Equality { get; set; }
    }
}
