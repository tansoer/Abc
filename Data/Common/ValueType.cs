using System.ComponentModel;

namespace Abc.Data.Common {
    public enum ValueType {
        [Description("Unspecified variable")] Unspecified = 0,
        [Description("Boolean variable")] Boolean = 1,
        [Description("String variable")] String = 2,
        [Description("Integer variable")] Integer = 3,
        [Description("Decimal variable")] Decimal = 4,
        [Description("Double variable")] Double = 5,
        [Description("DateTime variable")] DateTime = 6,
        [Description("Quantity variable")] Quantity = 7,
        [Description("Money variable")] Money = 8,
        [Description("Error variable")] Error = 9
    }
}