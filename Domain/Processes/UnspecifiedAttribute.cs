using Abc.Data.Processes;

namespace Abc.Domain.Processes {
    public sealed class UnspecifiedAttribute :AttributeValue {
        public UnspecifiedAttribute() : this(null) { }
        public UnspecifiedAttribute(AttributeValueData d) : base(d) { }
    }
}