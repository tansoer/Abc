
using System;
using System.Linq;

namespace Abc.Aids.Values {

    public static class TypesAre {
        public static bool Null(params object[] objects) {
            if (objects is null) return true;

            return !objects.Any() || objects.All(o => o is null);
        }
        public static bool DateTime(params object[] values) {
            return isTypeOf(values, TypeIs.DateTime);
        }
        public static bool String(params object[] values) {
            return isTypeOf(values, TypeIs.String);
        }
        public static bool Bool(params object[] values) => isTypeOf(values, TypeIs.Bool);
        public static bool Expression(params object[] values) => isTypeOf(values, TypeIs.Expression);
        public static bool Long(params object[] values) => isTypeOf(values, TypeIs.Long);
        public static bool Int(params object[] values) => isTypeOf(values, TypeIs.Int);
        public static bool Short(params object[] values) => isTypeOf(values, TypeIs.Short);
        public static bool SByte(params object[] values) => isTypeOf(values, TypeIs.SByte);
        public static bool ULong(params object[] values) => isTypeOf(values, TypeIs.ULong);
        public static bool UInt(params object[] values) => isTypeOf(values, TypeIs.UInt);
        public static bool UShort(params object[] values) => isTypeOf(values, TypeIs.UShort);
        public static bool Byte(params object[] values) => isTypeOf(values, TypeIs.Byte);
        public static bool Double(params object[] values) => isTypeOf(values, TypeIs.Double);
        public static bool Float(params object[] values) => isTypeOf(values, TypeIs.Float);
        public static bool Decimal(params object[] values) => isTypeOf(values, TypeIs.Decimal);
        public static bool AnyDouble(params object[] values) => isTypeOf(values, TypeIs.AnyDouble);
        public static bool AnyDecimal(params object[] values) => isTypeOf(values, TypeIs.AnyDecimal);
        public static bool TimeSpan(params object[] values) => isTypeOf(values, TypeIs.TimeSpan);
        public static bool AnyInt(params object[] values) => isTypeOf(values, TypeIs.AnyInt);
        public static bool AnyLong(params object[] values) => isTypeOf(values, TypeIs.AnyLong);
        private static bool isTypeOf(object[] values, Func<object, bool> isTypeOf) {
            if (values is null) return false;
            var a = values.Any();
            var b = values.All(isTypeOf);
            return a && b;
        }
    }
}
