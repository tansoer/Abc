using Abc.Data.Parties;

namespace Abc.Domain.Parties.Names {

    public sealed class OrganizationName :PartyName {

        public OrganizationName() : this(null) { }
        public OrganizationName(PartyNameData d) : base(d) { }

        public override string ToString() => get(Name);
    }
}
