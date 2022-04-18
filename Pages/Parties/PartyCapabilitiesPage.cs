using Abc.Data.Roles;
using Abc.Domain.Parties.Capabilities;
using Abc.Facade.Parties;
using Abc.Pages.Common;

namespace Abc.Pages.Parties {
    public sealed class PartyCapabilitiesPage :ViewFactoryPage<PartyCapabilitiesPage, IPartyCapabilitiesRepo,
        PartyCapability, PartyCapabilityView, PartyCapabilityData, PartyCapabilityViewFactory> {
        protected override string title => PartyTitles.PartyCapabilities;
        public PartyCapabilitiesPage(IPartyCapabilitiesRepo r) : base(r) { }
        protected override void tableColumns() {
            tableColumn(x => Item.RuleContextId);
        }
        protected internal override string pageUrl => PartyUrls.PartyCapabilities;
    }
}
