using Abc.Data.Processes;

namespace Abc.Domain.Processes {

    public sealed class Attribute :AttributeValue {
        public Attribute() : this(null) { }
        public Attribute(AttributeValueData d) : base(d) { }
        internal string processElementId => get(Data?.ProcessElementId);
    }
}

