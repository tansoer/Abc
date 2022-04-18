using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Methods;
using Abc.Data.Rules;
using Abc.Domain.Common;
using System;
using System.Linq.Expressions;

namespace Abc.Domain.Rules {
    public interface IRuleSet: IEntity<RuleSetData> {
        public IReadOnlyList<RuleOverride> Overrides { get; }
        public IReadOnlyList<RuleUsage> RuleUsages { get; }
        public IReadOnlyList<BaseRule> Rules { get; }
        public IVariable Evaluate(RuleContext c);
        public IVariable Evaluate(RuleContext c, params RuleOverride[] overrides) ;
        public IVariable Evaluate(RuleOverride o);
        public Expression ExportExpression<TClass>();
    }
    public sealed class RuleSet : Entity<RuleSetData>, IRuleSet {
        internal static string ruleSetId => nameOf<RuleUsageData>(x => x.RuleSetId);
        internal static string fixedFilter => nameOf<RuleOverrideData>(x => x.RuleSetId);
        public RuleSet() : this(null) { }
        public RuleSet(RuleSetData d) : base(d) { }
        public IReadOnlyList<RuleOverride> Overrides => list<IRuleOverridesRepo, RuleOverride>(fixedFilter, Id);
        public IReadOnlyList<RuleUsage> RuleUsages => list<IRuleUsagesRepo, RuleUsage>(ruleSetId, Id);
        public IReadOnlyList<BaseRule> Rules => list(RuleUsages,e => e.Rule);
        public IVariable Evaluate(RuleContext c) => Safe.Run(() => {
            IVariable result = new BooleanVariable(string.Empty, true);
            foreach (var r in Rules) {
                var x = r.Evaluate(c);
                result = result.And(x);
            }
            return result;
        }, new RuleError());
        public IVariable Evaluate(RuleContext c, params RuleOverride[] overrides) => Safe.Run(() => {
            IVariable result = new BooleanVariable(string.Empty, true);
            foreach (var r in Rules) {
                var o = overrides.FirstOrDefault(x => x.RuleId == r.Id);
                var b = o is null ? r.Evaluate(c) : o.RuleVariable;
                result = result.And(b);
            }
            return result;
        }, new RuleError());
        public IVariable Evaluate(RuleOverride o) => Safe.Run(() => {
            var ruleError = new RuleError();
            if (o is null) return ruleError;
            return o.RuleSetId != Id ? ruleError : o.RuleVariable;
        }, new RuleError());

        public Expression ExportExpression<TClass>() {
            var param = Expression.Parameter(typeof(TClass), "x");
            Expression body = Rules[0].ExportExpression(param);
            for (var i = 1; i < Rules.Count; i++)
                body = Expression.And(body, Rules[i].ExportExpression(param));
            return Expression.Lambda<Func<TClass, bool>>(body, param);
        }
    }
}
