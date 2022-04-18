
using System;
using Abc.Aids.Methods;

namespace Abc.Aids.Values {

    public static class Compare {

        public static bool IsGreater<T>(this T x, T y) where T : IComparable => safe(() => x.CompareTo(y) > 0);

        public static bool IsLess<T>(this T x, T y) where T : IComparable => safe(() => x.CompareTo(y) < 0);

        public static bool IsEqual<T>(this T x, T y) where T : IComparable => safe(() => x.CompareTo(y) == 0);

        public static bool IsNotGreater<T>(this T x, T y) where T : IComparable => safe(() => x.CompareTo(y) <= 0);

        public static bool IsNotLess<T>(this T x, T y) where T : IComparable => safe(() => x.CompareTo(y) >= 0);

        public static bool IsNotEqual<T>(this T x, T y) where T : IComparable => safe(() => x.CompareTo(y) != 0);

        internal static bool safe(Func<bool> f) => Safe.Run(f, false);

    }

}
