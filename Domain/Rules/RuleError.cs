using System;
using Abc.Aids.Enums;
using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public sealed class RuleError : BaseVariable<RuleError, string> {

        public RuleError(RuleElementData d = null) : base(d ?? new RuleElementData {
            RuleElementType = RuleElementType.Error,
            ValidFrom = DateTime.Now
        }) { }

        public RuleError(string name, string definition = null, string code = null) : this(new RuleElementData {
            Name = name,
            Code = code ?? string.Empty,
            Details = definition ?? string.Empty,
            RuleElementType = RuleElementType.Error,
            ValidFrom = DateTime.Now
        }) { }

        protected override string toValue(string v) => v ?? string.Empty;

        public override IVariable IsEqual(IVariable v) => this;

        public override IVariable IsGreater(IVariable v) => this;

        public override IVariable IsLess(IVariable v) => this;

    }

}
