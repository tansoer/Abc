using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Methods;

namespace Abc.Aids.Extensions {
    public static class Lists {

        public static IEnumerable<T> OrderBy<T>(IEnumerable<T> list, Func<T, string> func)
            => Safe.Run( () => list.OrderBy(func), new List<T>() as IEnumerable<T>, true);

        public static IEnumerable<T> Distinct<T>(IEnumerable<T> list)
            => Safe.Run(list.Distinct, new List<T>(), true);

        public static IEnumerable<TTo> Convert<TFrom, TTo>(IEnumerable<TFrom> list,
            Func<TFrom, TTo> func) => Safe.Run(() => list.Select(func),
                new List<TTo>(), true);
        public static string ToSeparatedString<T>(this IEnumerable<T> list, char splitter) 
            => Safe.Run(() => string.Join(splitter, Convert(list, x => x.ToString())),string.Empty);
    }
}




