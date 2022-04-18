using Abc.Aids.Enums;
using Abc.Data.Parties;

namespace Abc.Domain.Parties {
    public static class PartyFactory {
        public static IParty Create(PartyData d) => d?.PartyType switch {
                PartyType.Person => new Person(d),
                PartyType.Organization => new Organization(d),
                _ => new Person(d)
            };

        public static PartyData Create(IParty obj) => obj.Data;
    }
}
