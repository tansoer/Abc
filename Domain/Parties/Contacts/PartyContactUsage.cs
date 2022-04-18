using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Parties;
using Abc.Domain.Parties.Attributes;

namespace Abc.Domain.Parties.Contacts {
    public sealed class PartyContactUsage : BasePartyAttribute<PartyContactUsageData> {
        public PartyContactUsage() : this(null) { }
        public PartyContactUsage(PartyContactUsageData d) : base(d) { }
        public IPartyContact Contact => item<IPartyContactsRepo, IPartyContact>(contactId);
        public ContactUsageType Type => item<IClassifiersRepo, IClassifier>(typeId) as ContactUsageType;
        internal string contactId => get(Data?.Name);
        public string typeId => get(Data?.Code);
    }
}