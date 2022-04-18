using System;

namespace Abc.Domain.Common {
    public static class Unspecified {
        public static string String => Aids.Constants.Word.Unspecified;
        public static DateTime ValidFromDate => DateTime.MinValue;
        public static DateTime ValidToDate => DateTime.MaxValue;
        public static double Double => double.NaN;
        public static decimal Decimal => decimal.MaxValue;
        public static int Integer => int.MaxValue;
        public static uint UInt => uint.MaxValue;
        public static byte Byte => byte.MaxValue;
        public static bool IsUnspecified(string s) => string.IsNullOrWhiteSpace(s) || (s.Equals(String));
        public static bool IsUnspecified(byte b) => b.Equals(Byte);
        public static bool IsUnspecified(uint i) => i.Equals(UInt);
        public static bool IsUnspecified(int i) => i.Equals(Integer);
        public static bool IsUnspecified(double d) => d.Equals(Double);
        public static bool IsUnspecified(decimal d) => d.Equals(Decimal);
        public static bool IsUnspecified(DateTime d, bool isValidFrom ) 
            => isValidFrom? d.Equals(ValidFromDate): d.Equals(ValidToDate);
    }
}