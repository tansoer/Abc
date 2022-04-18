using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public sealed class UnspecifiedVariable : BaseVariable<UnspecifiedVariable, string> {

        protected override string toValue(string v) => v ?? string.Empty;

        public UnspecifiedVariable(RuleElementData d = null) : base(d) { }

        public override IVariable IsEqual(IVariable v) => this;

        public override IVariable IsGreater(IVariable v) => this;

        public override IVariable IsLess(IVariable v) => this;

    }

}