using Abc.Pages.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Parties {
    [TestClass]
    public class PartyTitlesTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(PartyTitles);
        [TestMethod] public void RolesTest() => areEqual("Party roles", PartyTitles.Roles);
        [TestMethod] public void OrganizationTypesTest() => areEqual("Organization types", PartyTitles.OrganizationTypes);
        [TestMethod] public void BodyMetricsTest() => areEqual("Body metrics", PartyTitles.BodyMetrics);
        [TestMethod] public void BodyMetricTypesTest() => areEqual("Body metric types", PartyTitles.BodyMetricTypes);
        [TestMethod] public void PartySignaturesTest() => areEqual("Party signatures", PartyTitles.PartySignatures);
        [TestMethod] public void PartyContactsTest() => areEqual("Party contacts", PartyTitles.PartyContacts);
        [TestMethod] public void PartiesTest() => areEqual("Parties", PartyTitles.Parties);
        [TestMethod] public void NamesTest() => areEqual("Party names", PartyTitles.Names);
        [TestMethod] public void ContactUsageTypesTest() => areEqual("Party contact usage types", PartyTitles.ContactUsageTypes);
        [TestMethod] public void PersonNamePrefixTypesTest() => areEqual("Person name prefix types", PartyTitles.PersonNamePrefixTypes);
        [TestMethod] public void PersonNameSuffixTypesTest() => areEqual("Person name suffix types", PartyTitles.PersonNameSuffixTypes);
        [TestMethod] public void PersonNamePrefixesTest() => areEqual("Person name prefixes", PartyTitles.PersonNamePrefixes);
        [TestMethod] public void PersonNameSuffixesTest() => areEqual("Person name suffixes", PartyTitles.PersonNameSuffixes);
        [TestMethod] public void PartyNameUseTypesTest() => areEqual("Party name use types", PartyTitles.PartyNameUseTypes);
        [TestMethod] public void SignedEntityTypesTest() => areEqual("Signed entity types", PartyTitles.SignedEntityTypes);
        [TestMethod] public void RegisteredIdentifierTypesTest() => areEqual("Registered identifier types", PartyTitles.RegisteredIdentifierTypes);
        [TestMethod] public void PartyCapabilityTypesTest() => areEqual("Party capability types", PartyTitles.PartyCapabilityTypes);
        [TestMethod] public void RegistrationAuthoritiesTest() => areEqual("Registration authorities", PartyTitles.RegistrationAuthorities);
        [TestMethod] public void PersonEthnicitiesTest() => areEqual("Person ethnicities", PartyTitles.PersonEthnicities);
        [TestMethod] public void PreferencesTest() => areEqual("Party preferences", PartyTitles.Preferences);
        [TestMethod] public void RegisteredIdentifiersTest() => areEqual("Party registered identifiers", PartyTitles.RegisteredIdentifiers);
        [TestMethod] public void PartyCapabilitiesTest() => areEqual("Party capabilities", PartyTitles.PartyCapabilities);
        [TestMethod] public void PartyContactUsagesTest() => areEqual("Party contact usages", PartyTitles.PartyContactUsages);
    }
}
