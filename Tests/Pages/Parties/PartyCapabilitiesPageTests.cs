using Abc.Data.Roles;
using Abc.Domain.Parties.Capabilities;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Parties {
    [TestClass]
    public class PartyCapabilitiesPageTests : SealedViewFactoryPageTests<
        PartyCapabilitiesPage,
        IPartyCapabilitiesRepo,
        PartyCapability, 
        PartyCapabilityView, 
        PartyCapabilityData, 
        PartyCapabilityViewFactory> {
        protected override PartyCapability toObject(PartyCapabilityData d) => new (d);
        protected override PartyCapabilitiesPage createObject() => new (null);
        protected override string pageTitle => PartyTitles.PartyCapabilities;
        protected override string pageUrl => PartyUrls.PartyCapabilities;
    }
}
