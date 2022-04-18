using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using Abc.Aids.Methods;

namespace Abc.Aids.Reflection {

    public static class GetMember {

        public static string Name<TClass>(Expression<Func<TClass, object>> ex) 
            => Safe.Run(() => name(ex?.Body), string.Empty);
        public static string Name<T, TResult>(Expression<Func<T, TResult>> ex) 
            => Safe.Run(() => name(ex?.Body), string.Empty);
        public static string Name<T>(Expression<Action<T>> ex) 
            => Safe.Run(() => name(ex?.Body), string.Empty);
        public static string DisplayName<T>(Expression<Func<T, object>> ex) 
            => Safe.Run(() => {
                var name = Name(ex);
                return DisplayName<T>(name);
            }, string.Empty);
        public static string DisplayName<T, TResult>(Expression<Func<T, TResult>> ex) 
            => Safe.Run(() => {
                var name = Name(ex);
                return DisplayName<T>(name);
            }, string.Empty);
        public static string DisplayName<T>(string propertyName)=> DisplayName(propertyName, typeof(T));
        public static string DisplayName(string propertyName, Type t)
            => Safe.Run(() => {
                var name = propertyName ?? string.Empty;
                var p = GetClass.Property(name, t);
                var list = p?.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                if (list is null || list.Length < 1) return name;
                var a = list.Cast<DisplayNameAttribute>().Single();
                return a?.DisplayName ?? name;
            }, string.Empty);
        public static bool IsRequired(string propertyName, Type t)
            => Safe.Run(() => {
                var name = propertyName ?? string.Empty;
                var p = GetClass.Property(name, t);
                var list = p?.GetCustomAttributes(typeof(RequiredAttribute), true);
                return (list?.Length?? 0) ==1;
            }, false);
        public static bool IsRequired<T>(string propertyName) => IsRequired(propertyName, typeof(T));
        public static bool IsRequired<T>(Expression<Func<T, object>> ex)
            => Safe.Run(() => {
                var name = Name(ex);
                return IsRequired<T>(name);
            }, false);
        public static string ColumnName<T>(Expression<Func<T, object>> ex)
            => Safe.Run(() => {
                var name = Name(ex);
                return ColumnName<T>(name);
             }, string.Empty);
        public static string ColumnName<T>(string propertyName) => ColumnName(propertyName, typeof(T));
        public static string ColumnName(string propertyName, Type t)
            => Safe.Run(() => {
                var name = propertyName ?? string.Empty;
                var p = GetClass.Property(name, t);
                var list = p?.GetCustomAttributes(typeof(ColumnAttribute), true);

                if (list is null || list.Length < 1) return name;
                var a = list.Cast<ColumnAttribute>().Single();
                return a?.Name ?? name;
            }, string.Empty);
        private static string name(Expression ex) {
            if (ex is MemberExpression member) return name(member);
            if (ex is MethodCallExpression method) return name(method);
            if (ex is UnaryExpression operand) return name(operand);

            return string.Empty;
        }
        private static string name(MemberExpression ex) => ex?.Member.Name ?? string.Empty;
        private static string name(MethodCallExpression ex) => ex?.Method.Name ?? string.Empty;
        private static string name(UnaryExpression ex) {
            if (ex?.Operand is MemberExpression member) return name(member);
            if (ex?.Operand is MethodCallExpression method) return name(method);
            return string.Empty;
        }
    }
}




