using System.ComponentModel;

namespace Abc.Data.Processes
{
    public enum AttributeValueType {
        Unspecified = 0,
        [Description("Attribute Value")] AttributeValue = 1,
        [Description("Possible Value")] PossibleValue = 2
    }
}