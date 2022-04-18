using Abc.Domain.Common;

namespace Abc.Domain.Quantities {

    public sealed class UnspecifiedUnit : Unit {
        public override double ToBase(in double amount) => Unspecified.Double;
        public override double FromBase(in double amount) => Unspecified.Double;
        protected override Unit multiply(DerivedUnit u, string n = null, string c = null, string d = null) => this;
        protected override Unit multiply(FactoredUnit u, string n = null, string c = null, string d = null) => this;
        protected override Unit multiply(FunctionedUnit u, string n = null, string c = null, string d = null) => this;
        protected override Unit toPower(in double power, string n = null, string c = null, string d = null) => this;
        protected override string formula(bool isLong = false) => Unspecified.String;
    }
}