using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Data.Rules;
using Abc.Domain.Common;

namespace Abc.Domain.Rules {

    public sealed class RuleContext : Entity<RuleContextData> {
        private static string contextId => nameOf<RuleElementData>(x => x.RuleContextId);
        private readonly List<IVariable> variables;
        public RuleContext() : this(null) { }
        public RuleContext(RuleContextData d) : base(d) { }
        public RuleContext(in object value) : this(new RuleContextData { Name = $"Value={value}", ValidFrom = DateTime.Now }) {
            variables = new List<IVariable>();
            var v = VariableFactory.Create("x", value, Id, true);
            variables.Add(v);
        }
        public string RuleId => get(Data?.RuleId);
        public string RuleSetId => get(Data?.RuleSetId);
        public BaseRule Rule => item<IRulesRepo, BaseRule>(RuleId);
        public IRuleSet RuleSet => item<IRuleSetsRepo, IRuleSet>(RuleSetId);
        public IReadOnlyList<IVariable> Variables => variables?.AsReadOnly() ?? getVariables(Id);
        internal static IReadOnlyList<IVariable> getVariables(string id) {
            var l = list<IRuleElementsRepo, IRuleElement>(contextId, id);
            return l.Select(x => x as IVariable).ToList().AsReadOnly();
        }
        public IVariable FindVariable(Operand o) {
            var r = Variables.FirstOrDefault(x => x.Name == o?.Name);
            return r;
        }
    }
}
