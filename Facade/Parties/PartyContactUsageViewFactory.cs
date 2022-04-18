using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Common;

namespace Abc.Facade.Parties
{
    public sealed class PartyContactUsageViewFactory :AbstractViewFactory<PartyContactUsageData, PartyContactUsage, PartyContactUsageView> {
        protected internal override PartyContactUsage toObject(PartyContactUsageData d) => new (d);
    }
}