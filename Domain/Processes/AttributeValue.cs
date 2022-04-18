using Abc.Aids.Calculator;
using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Values;

namespace Abc.Domain.Processes {

    public abstract class AttributeValue :Entity<AttributeValueData> {
        protected AttributeValue() : this(null) { }
        protected AttributeValue(AttributeValueData d) : base(d) { }
        public AttributeType Type => item<IAttributeTypesRepo, AttributeType>(AttributeTypeId);
        public string AttributeTypeId => get(Data?.AttributeTypeId);
        public IValue Value => ValueFactory.Create(Data?.Value);
    }
}
