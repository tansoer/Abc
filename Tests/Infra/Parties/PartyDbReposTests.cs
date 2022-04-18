using System;
using Abc.Domain.Common;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Capabilities;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Parties.Identifiers;
using Abc.Domain.Parties.Names;
using Abc.Domain.Parties.Preferences;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Roles;
using Abc.Infra.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class PartyDbReposTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(PartyDbRepos);

        [DataTestMethod]
        [DataRow(typeof(IBodyMetricsRepo))]
        [DataRow(typeof(IBodyMetricTypesRepo))]
        [DataRow(typeof(IPartySummariesRepo))]
        [DataRow(typeof(IPersonEthnicitiesRepo))]
        [DataRow(typeof(IPartyCapabilitiesRepo))]
        [DataRow(typeof(ICountriesRepo))]
        [DataRow(typeof(IPartyContactsRepo))]
        [DataRow(typeof(IPartyContactUsagesRepo))]
        [DataRow(typeof(IDeviceRegistrationsRepo))]
        [DataRow(typeof(IRegisteredIdentifiersRepo))]
        [DataRow(typeof(IRegistrationAuthoritiesRepo))]
        [DataRow(typeof(IPartyNamesRepo))]
        [DataRow(typeof(IPartyNameUsesRepo))]
        [DataRow(typeof(INameSuffixesRepo))]
        [DataRow(typeof(INamePrefixesRepo))]
        [DataRow(typeof(IPreferenceOptionsRepo))]
        [DataRow(typeof(IPreferencesRepo))]
        [DataRow(typeof(ISignedEntityTypesRepo))]
        [DataRow(typeof(IPartySignaturesRepo))]
        [DataRow(typeof(IAuthenticationsRepo))]
        [DataRow(typeof(IPartiesRepo))]
        public void RegisterTest(Type t) => Assert.IsNotNull(GetRepo.Instance(t));

    }

}
