using System.Linq.Expressions;
using Abc.Aids.Calculator;
using System.Reflection;
using Abc.Aids.Methods;
using System;

namespace Abc.Aids.Values {
    public static class Expr {
        public static Expression Compose(
            Expression param, Operation o, string propertyName, params IValue[] values) => Safe.Run(
                () => {
                    var p = value(propertyName, param);
                    var args = new Expression[1 + (values?.Length?? 0)];
                    args[0] = Expression.Convert(p, typeof(object));
                    for (var i = 0; i < values?.Length; i++)
                        args[i + 1] = Expression.Convert(value(values[i]), typeof(object));
                    var m = methodInfo(o);
                    var call = Expression.Call(type, m.Name, null, args);
                    var catchParam = Expression.Parameter(typeof(Exception));
                    var catchCall = Expression.Call(type, nameof(Compute.Exception), null, catchParam);
                    return Expression.TryCatch(call, Expression.Catch(catchParam, catchCall));
                }, error);
        public static Expression Compose(Expression param, Operation o, IValue x, params IValue[] values)
            => Safe.Run(() => Compose(param, o, getPropertyName(x), values), error);
        internal static string getPropertyName(IValue x) => x?.GetValue() as string;
        internal static Type type => typeof(Compute);
        internal static Expression error => 
            Expression.Call(type, nameof(Compute.Dummy), null);
        internal static ParameterExpression exprParam => Expression.Parameter(typeof(Exception));
        internal static CatchBlock catchExpr => Expression.Catch(exprParam, error);
        internal static MethodInfo methodInfo(Operation o) 
            => Safe.Run(() => type.GetMethod(o.ToString()), (MethodInfo) null);
        internal static Expression value(IValue v) 
            => Safe.Run(() => Expression.Constant(v.GetValue()), (Expression)null);
        internal static Expression value(string propertyName, Expression param) 
            => Safe.Run(() => Expression.Property(param, propertyName), (Expression)null);
    }
}