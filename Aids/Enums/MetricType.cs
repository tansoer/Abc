namespace Abc.Aids.Enums
{
    public enum MetricType {
        Unspecified = 0,
        String = 1,
        Quantity = 2
    }

    public static class MetricTypeExtensions {
        public static bool IsString(this MetricType t) => t == MetricType.String;
        public static bool IsQuantity(this MetricType t) => t == MetricType.Quantity;
    }
}