using Abc.Data.Quantities;
using Abc.Domain.Common;

namespace Abc.Domain.Quantities {

    public abstract class BaseTerm : BaseEntity<CommonTermData>, ITerm {
        protected BaseTerm() : this(null) { }
        protected BaseTerm(CommonTermData d) : base(d) { }
        public double Power => data?.Power ?? 0;
        protected internal string formula(string s) => Power == 1 ? s : $"{s}^{Power}";
    }
}