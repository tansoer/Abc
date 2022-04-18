using System;
using System.Linq.Expressions;
using Abc.Aids.Reflection;

namespace Abc.Aids.Services {
    public sealed class RowClassMapper {
        public RowClassMapper() { }
        public RowClassMapper(string name, object value, RowClassMapperType valueType, string formatStr = null) {
            Name = name;
            Value = value;
            ValueType = valueType;
            FormatStr = formatStr;
        }
        public static RowClassMapper Create<T>(Expression<Func<T, object>> name, object value
            , RowClassMapperType valueType, string formatStr = null)
            => new (GetMember.Name(name), value, valueType, formatStr);
        public string Name { get; set; }
        public object Value { get; set; }
        public RowClassMapperType ValueType { get; set; }
        public string FormatStr { get; set; }
    }
}
