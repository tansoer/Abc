using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {
    public class PersonEthnicityViewFactory :
        AbstractViewFactory<PersonEthnicityData, PersonEthnicity, PersonEthnicityView> {

        protected internal override PersonEthnicity toObject(PersonEthnicityData d)
            => new PersonEthnicity(d);
    }
}