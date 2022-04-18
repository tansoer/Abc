using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Methods;
using Abc.Aids.Reflection;
using Abc.Data.Rules;
using Abc.Domain.Common;
using System.Linq.Expressions;
using Abc.Aids.Calculator;

namespace Abc.Domain.Rules {
    public abstract class BaseRule :Entity<RuleData> {
        internal static string incorrectRuleId => "RuleId is not specified in Evaluate";
        internal static string overrideIsNull => "Override is not specified in Evaluate";
        internal static string exceptionMessage(string s) => $"Exception in Evaluate: {s}";
        protected BaseRule() :this(null) { }
        protected BaseRule(RuleData r) :base(r) { }
        public IReadOnlyList<IRuleElement> Elements => new GetFrom<IRuleElementsRepo, IRuleElement>()
            .ListBy(GetMember.Name<RuleElementData>(x => x.RuleId), Id);
        public IVariable Evaluate(RuleContext c) => evaluate(Elements, c);
        public IVariable Evaluate(RuleOverride o) => Safe.Run(() => {
            if (o is null) return new RuleError(Name, overrideIsNull);
            return o.RuleId != Id ? new RuleError(Name, incorrectRuleId) : o.RuleVariable;
        }, x => new RuleError(Name, exceptionMessage(x)));
        public object Evaluate<T>(in T value) {
            var v = Evaluate(new RuleContext(value));
            return v.GetValue();
        }

        protected static IVariable evaluate(IEnumerable<IRuleElement> elements, RuleContext c) => Safe.Run(() => {
            if (c is null) throw new ApplicationException();
            var m = new VariableCalculator();
            foreach (var e in elements.OrderBy(x => x.Index)) {
                if (e is Operator o) m.Perform(o.Operation);
                else if (e is IVariable) m.Set(e);
                else m.Set(c.FindVariable(e as Operand));
            }
            return m.Get() as IVariable;
        }, new RuleError());
        public string Formula() => Safe.Run(() => {
            var s = string.Empty;
            foreach (var e in Elements.OrderBy(x => x.Index)) {
                if (e is Operator o) s = $"{s} {o.Name}";
                else if (e is IVariable v) s = $"{s} {v.GetValue()}"; 
                else s = $"{s} {(e as Operand).Name}";
            }
            return s.Trim();
        }, x => exceptionMessage(x));


        public Expression ExportExpression(ParameterExpression param) => exportExpression(Elements, param);

        protected static Expression exportExpression(IEnumerable<IRuleElement> elements, ParameterExpression param) 
            => Safe.Run(() => {
            var m = new VariableCalculator();
            foreach (var e in elements.OrderBy(x => x.Index)) {
                if (e is Operator o) {
                    if (o.Operation == Operation.And) m.Set(Expression.And((Expression)m.Get(), (Expression)m.Get()));
                    else m.Perform(o.Operation, param);
                } else if (e is IVariable) m.Set(e);
            }

            return (Expression)m.Get();
        }, Expression.Constant(null));
    }
}

