using Abc.Data.Processes;
using Abc.Domain.Common;
using System.Collections.Generic;

namespace Abc.Domain.Processes {
    public sealed class AttributeType :Entity<AttributeTypeData> {
        public AttributeType() : this(null) { }
        public AttributeType(AttributeTypeData d) : base(d) { }
        public string ElementTypeId => get(Data?.ElementTypeId);
        public bool IsMandatory => get(Data?.IsMandatory);
        public IReadOnlyList<AttributeValue> AttributeValues
            => list<IAttributeValuesRepo, AttributeValue>(typeId, Id);
        public IReadOnlyList<AttributePossibleValue> PossibleValues 
            => list<AttributePossibleValue, AttributeValue>(AttributeValues);
        internal static string typeId => nameOf<AttributeValueData>(x => x.AttributeTypeId); 
    }
}
