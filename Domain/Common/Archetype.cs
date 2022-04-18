using Abc.Aids.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Domain.Common {
    public abstract class Archetype {
        protected internal static string get(string v) => v ?? Unspecified.String;

        protected internal static decimal get(decimal? d) => d ?? Unspecified.Decimal;

        protected internal static double get(double? d) => d ?? Unspecified.Double;

        protected internal static uint get(uint? i) => i ?? Unspecified.UInt;

        protected internal static int get(int? i) => i ?? Unspecified.Integer;

        protected internal static byte get(byte? b) => b ?? Unspecified.Byte;

        protected internal static bool get(bool? b) => b ?? false;

        protected internal static DateTime get(DateTime? d, bool valFrom = true) => d ?? def(valFrom);

        protected internal static TObj item<TRepo, TObj>(string id)
            where TRepo : IRepo<TObj> => new GetFrom<TRepo, TObj>().ById(id);

        protected internal static TObj item<TObj>(IEnumerable<TObj> l, Func<TObj, bool> f)
            where TObj : new() => l.FirstOrDefault(f) ?? new TObj();

        protected internal static bool add<TRepo, TObj>(TObj o) where TRepo : IRepo<TObj>
            => GetRepo.Instance<TRepo>().Add(o);

        protected internal static bool update<TRepo, TObj>(TObj o) where TRepo : IRepo<TObj>
            => GetRepo.Instance<TRepo>().Update(o);

        protected internal static bool delete<TRepo, TObj>(TObj o) where TRepo : IRepo<TObj>
            where TObj : IEntity
            => GetRepo.Instance<TRepo>().Delete(o?.Id);

        protected internal static IReadOnlyList<TObj>
            list<TRepo, TObj>(string propName, string propValue)
            where TRepo : IRepo<TObj>
            => new GetFrom<TRepo, TObj>()?.ListBy(propName, propValue)
            ?? new List<TObj>().AsReadOnly();

        protected internal static IReadOnlyList<TResult>
            list<TSource, TResult>(IEnumerable<TSource> l, Func<TSource, TResult> f)
            => (l?.Select(f).ToList()?? new List<TResult>()).AsReadOnly();

        protected internal static IReadOnlyList<TSource>
            list<TSource>(IEnumerable<TSource> l, Func<TSource, bool> f)
            => (l?.Where(f).ToList()?? new List<TSource>()).AsReadOnly();

        protected internal static IReadOnlyList<TAs>
            list<TAs, TIs>(IEnumerable<TIs> l)
            where TAs : TIs
            => (l?.OfType<TAs>().ToList()?? new List<TAs>()).AsReadOnly();

        protected internal static string nameOf<T>(Expression<Func<T, object>> ex)
            => GetMember.Name(ex);

        protected internal static bool isNull(string s)
            => (s == Unspecified.String) || string.IsNullOrWhiteSpace(s?.Trim());

        protected internal static bool isNull(uint? i)
            => (i == Unspecified.UInt) || (i is null);

        protected internal static bool isNull(object o) => o is null;

        protected internal static bool isNull(DateTime? d, bool valFrom = true)
            => (d == def(valFrom)) || (d is null);

        protected internal static uint toNumber(string s)
            => uint.TryParse(s, out uint i) ? i : Unspecified.UInt;

        protected internal static DateTime def(bool valFrom)
            => valFrom ? Unspecified.ValidFromDate : Unspecified.ValidToDate;
    }
}
