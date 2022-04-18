using Abc.Aids.Enums;
using Abc.Data.Parties;

namespace Abc.Domain.Parties.Names {

    public class PartyNameFactory {
        public static PartyName Create(PartyNameData d) => d?.PartyType switch {
            PartyType.Organization => new OrganizationName(d),
            PartyType.Person => new PersonName(d),
            _ => new OrganizationName(d)
        };
        public static PartyNameData Create(PartyName obj) => 
             obj?.Data?? new PartyNameData() ;
    }
}