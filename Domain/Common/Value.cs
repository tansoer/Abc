using Abc.Aids.Classes;
using Abc.Aids.Methods;
using Abc.Data.Common;
using System;

namespace Abc.Domain.Common {
    public abstract class Value<TData>: Archetype
        where TData : class, new() {
        internal protected TData data;
        internal static Guid guid;
        protected internal Value() : this(null) { }
        protected internal Value(TData d) => SetData(d);
        public void SetData(TData d) { if (IsUnspecified) data = d ?? new TData(); }
        public TData Data {
            get {
                if (data is null) return null;
                var d = new TData();
                Copy.Members(data, d);
                return d;
            }
        }
        public bool IsUnspecified => data is null || isUnspecified();
        protected internal virtual bool isUnspecified() => arePropertiesEqual(data, new TData());
        internal static bool arePropertiesEqual(TData a, TData b) => arePropertiesEqual<TData>(a, b);
        internal static bool arePropertiesEqual(TData a, TData b, params string[] except) 
            => arePropertiesEqual<TData>(a, b, except);
        internal static bool arePropertiesEqual<T>(T a, T b) =>
            AreProperties.Equal(a, b, nameOf<EntityBaseData>(x => x.Id))
            && AreProperties.Guids(a, b, nameOf<EntityBaseData>(x => x.Id));
        internal static bool arePropertiesEqual<T>(T a, T b, params string[] except) =>
            AreProperties.Equal(a, b, except);
    }
}
