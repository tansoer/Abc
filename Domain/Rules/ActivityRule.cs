using System.Collections.Generic;
using Abc.Aids.Methods;
using Abc.Aids.Reflection;
using Abc.Data.Rules;
using Abc.Domain.Common;

namespace Abc.Domain.Rules {
    public sealed class ActivityRule :BaseRule {
        public ActivityRule() :this(null) { }
        public ActivityRule(RuleData d) :base(d) { }
        public IReadOnlyList<IRuleElement> Activity => new GetFrom<IRuleElementsRepo, IRuleElement>()
            .ListBy(GetMember.Name<RuleElementData>(x => x.ActivityId), Id);
        public IReadOnlyList<IRuleElement> Context => new GetFrom<IRuleElementsRepo, IRuleElement>()
            .ListBy(GetMember.Name<RuleElementData>(x => x.RuleContextId), Id);
        public IVariable Perform() => Safe.Run(() => {
            var d = new RuleContextData {Id = Id};
            var c = new RuleContext(d);
            return evaluate(Activity, c);
        }, new RuleError());
        public IVariable Perform(RuleContext c) => evaluate(Activity, c);
        public IVariable Perform(RuleContext forRule, IVariable value, RuleContext forActivity) => Safe.Run(() => {
            var v = evaluate(Elements, forRule);
            if (v is null) return new RuleError();
            if (value is null) return new RuleError();
            return v.GetValue().Equals(value.GetValue()) ? evaluate(Activity, forActivity) : v.IsEqual(value);
        }, new RuleError());
    }
}
