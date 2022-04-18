using System.Collections.Generic;
using Abc.Aids.Enums;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;

namespace Abc.Domain.Parties.Names {

    public abstract class PartyName : BasePartyAttribute<PartyNameData>, IPartyName {

        protected PartyName() : this(null) { }
        protected PartyName(PartyNameData d) : base(d) { }

        public NameType NameType => Data?.NameType ?? NameType.Undefined;
        public IReadOnlyList<PartyNameUse> NameUse => list<IPartyNameUsesRepo, PartyNameUse>(partyNameId, Id);

        internal static string partyNameId => nameOf<PartyNameUseData>(x => x.PartyNameId);
    }
}
