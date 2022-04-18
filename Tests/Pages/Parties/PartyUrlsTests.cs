using Abc.Pages.Classifiers;
using Abc.Pages.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Parties {

    [TestClass]
    public class PartyUrlsTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(PartyUrls);
        [TestMethod] public void RolesTest() => areEqual("/Roles/Roles", PartyUrls.Roles);
        [TestMethod] public void OrganizationTypesTest() => areEqual("/Parties/OrganizationTypes", PartyUrls.OrganizationTypes);
        [TestMethod] public void BodyMetricsTest() => areEqual("/Parties/BodyMetrics", PartyUrls.BodyMetrics);
        [TestMethod] public void BodyMetricTypesTest() => areEqual("/Parties/BodyMetricTypes", PartyUrls.BodyMetricTypes);
        [TestMethod] public void PartySignaturesTest() => areEqual("/Parties/PartySignatures", PartyUrls.PartySignatures);
        [TestMethod] public void PartyContactsTest() => areEqual("/Parties/PartyContacts", PartyUrls.PartyContacts);
        [TestMethod] public void PartiesTest() => areEqual("/Parties/Parties", PartyUrls.Parties);
        [TestMethod] public void NamesTest() => areEqual("/Parties/Names", PartyUrls.Names);
        [TestMethod] public void PersonNameSuffixesTest() => areEqual("/Parties/Suffixes", PartyUrls.PersonNameSuffixes);
        [TestMethod] public void PersonNamePrefixesTest() => areEqual("/Parties/Prefixes", PartyUrls.PersonNamePrefixes);
        [TestMethod] public void PersonEthnicitiesTest() => areEqual("/Parties/PersonEthnicities", PartyUrls.PersonEthnicities);
        [TestMethod] public void SignedEntityTypesTest() => areEqual("/Parties/SignedEntityTypes", PartyUrls.SignedEntityTypes);
        [TestMethod] public void ContactUsageTypesTest() => areEqual("/Parties/PartyContactUsageTypes", PartyUrls.ContactUsageTypes);
        [TestMethod] public void PartyNameUseTypesTest() => areEqual("/Parties/PartyNameUseTypes", PartyUrls.PartyNameUseTypes);
        [TestMethod] public void PartyCapabilityTypesTest() => areEqual("/Parties/PartyCapabilityTypes", PartyUrls.PartyCapabilityTypes);
        [TestMethod] public void RegisteredIdentifiersTest() => areEqual("/Parties/RegisteredIdentifiers", PartyUrls.RegisteredIdentifiers);
        [TestMethod] public void PartyCapabilitiesTest() => areEqual("/Parties/PartyCapabilities", PartyUrls.PartyCapabilities);
        [TestMethod] public void PreferencesTest() => areEqual("/Parties/Preferences", PartyUrls.Preferences);
        [TestMethod] public void PartyContactUsagesTest() => areEqual("/Parties/PartyContactUsages", PartyUrls.PartyContactUsages);
    }
}
