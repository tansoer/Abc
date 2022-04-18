using System.Linq;
using Abc.Infra.Parties;
using Abc.Infra.Parties.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties.Initializers {

    [TestClass]
    public class PartyDbInitializerTests : DbInitializerTests<PartyDb> {

        public PartyDbInitializerTests() {
            type = typeof(PartyDbInitializer);
            db = new PartyDb(options);
            removeAll(db.BodyMetricTypes);
            removeAll(db.PartyContactUsages);
            removeAll(db.PartyContacts);
            removeAll(db.PartyNames);
            removeAll(db.PersonNamePrefixes);
            removeAll(db.SignedEntityTypes);
            removeAll(db.Parties);
            removeAll(db.PartyNameUses);
            removeAll(db.PreferenceOptions);
            removeAll(db.Preferences);
            removeAll(db.PartyAuthentications);
            removeAll(db.PartySignatures);
            removeAll(db.PersonNameSuffixes);
            removeAll(db.TelecomDeviceRegistrations);
            removeAll(db.Identifiers);
            removeAll(db.RegistrationAuthorities);
            removeAll(db.PartyCapabilities);
            removeAll(db.Countries);
            removeAll(db.PartySummaries);
            removeAll(db.PersonEthnicities);
            removeAll(db.BodyMetrics);
            PartyDbInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void BodyMetricTypesTest() => areNotEqual(0, db.BodyMetricTypes.Count());
        [TestMethod] public void PartyContactUsagesTest() => areNotEqual(0, db.PartyContactUsages.Count());
        [TestMethod] public void PartyContactsTest() => areNotEqual(0, db.PartyContacts.Count());
        [TestMethod] public void PartyNamesTest() => areNotEqual(0, db.PartyNames.Count());
        [TestMethod] public void PersonNamePrefixesTest() => areNotEqual(0, db.PersonNamePrefixes.Count());
        [TestMethod] public void SignedEntityTypesTest() => areNotEqual(0, db.SignedEntityTypes.Count());
        [TestMethod] public void PartiesTest() => areNotEqual(0, db.Parties.Count());
        [TestMethod] public void PartyNameUsesTest() => areEqual(0, db.PartyNameUses.Count());
        [TestMethod] public void PreferenceOptionsTest() => areEqual(0, db.PreferenceOptions.Count());
        [TestMethod] public void PreferencesTest() => areEqual(0, db.Preferences.Count());
        [TestMethod] public void PartyAuthenticationsTest() => areEqual(0, db.PartyAuthentications.Count());
        [TestMethod] public void PartySignaturesTest() => areEqual(0, db.PartySignatures.Count());
        [TestMethod] public void PersonNameSuffixesTest() => areEqual(0, db.PersonNameSuffixes.Count());
        [TestMethod] public void TelecomDeviceRegistrationsTest()
              => areEqual(0, db.TelecomDeviceRegistrations.Count());
        [TestMethod] public void IdentifiersTest() => areEqual(0, db.Identifiers.Count());
        [TestMethod] public void RegistrationAuthoritiesTest() => areEqual(0, db.RegistrationAuthorities.Count());
        [TestMethod] public void PartyCapabilitiesTest() => areEqual(0, db.PartyCapabilities.Count());
        [TestMethod] public void CountriesTest() => isTrue(135 <= db.Countries.Count(), $"{135} > {db.Countries.Count()}");
        [TestMethod] public void PartySummariesTest() => areEqual(0, db.PartySummaries.Count());
        [TestMethod] public void PersonEthnicitiesTest() => areEqual(0, db.PersonEthnicities.Count());
        [TestMethod] public void BodyMetricsTest() => areEqual(0, db.BodyMetrics.Count());
    }
}