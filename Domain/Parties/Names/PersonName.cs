using System.Collections.Generic;
using Abc.Data.Parties;

namespace Abc.Domain.Parties.Names {
    public sealed class PersonName : PartyName {

        public PersonName() : this(null) { }
        public PersonName(PartyNameData d) : base(d) { }

        public IReadOnlyList<NamePrefix> Prefixes => list<INamePrefixesRepo, NamePrefix>(nameId, Id);
        public IReadOnlyList<NameSuffix> Suffixes => list<INameSuffixesRepo, NameSuffix>(nameId, Id);

        public string FamilyName => Name;
        public string GivenName => get(Data?.GivenName);
        public string MiddleName => get(Data?.MiddleName);
        public string PreferredName => get(Data?.PreferredName);
        public override string ToString() => personName;

        internal string personName => $"{firstName} {lastName}".Trim();
        internal string lastName => isNull(firstName) ? FamilyName : familyName;
        internal string familyName => isNull(FamilyName) ? string.Empty : FamilyName;
        internal string firstName => isNull(PreferredName) ? middleName: PreferredName;
        internal string middleName => isNull(MiddleName) ? givenName : $"{givenName} {MiddleName}";
        internal string givenName => isNull(GivenName) ? string.Empty: GivenName;
        internal static string nameId => nameOf<NamePrefixData>(x => x.NameId);
    }
}
