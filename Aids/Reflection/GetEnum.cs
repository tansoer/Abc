using System;
using Abc.Aids.Methods;

namespace Abc.Aids.Reflection {

    public static class GetEnum {

        public static int Count<T>() => Count(typeof(T));

        public static T Value<T>(int i)
            => Safe.Run(() => (T) (Value(typeof(T), i)?? default(T)), default(T));

        public static int Count(Type type)
            => Safe.Run(() => Enum.GetValues(type).Length, -1);

        public static object Value(Type type, int i)
            => Safe.Run(() => {
                var values = Enum.GetValues(type);
                if (i > values.Length - 1) i = values.Length - 1;
                if (i < 0) i = 0;
                return values.GetValue(i);
            }, null);
    }
}



