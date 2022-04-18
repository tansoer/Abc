using Abc.Data.Processes;

namespace Abc.Domain.Processes
{
    public static class AttributeValuesFactory {
        public static AttributeValue Create(AttributeValueData d) => d?.Type switch {
            AttributeValueType.AttributeValue => new Attribute(d),
            AttributeValueType.PossibleValue => new AttributePossibleValue(d),
            _ => new UnspecifiedAttribute(d)
        };
    }
}