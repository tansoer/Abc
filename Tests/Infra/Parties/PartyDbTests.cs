using System;
using System.Linq;
using System.Linq.Expressions;
using Abc.Aids.Reflection;
using Abc.Data.Currencies;
using Abc.Data.Parties;
using Abc.Data.Roles;
using Abc.Facade.Parties;
using Abc.Infra;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {
    [TestClass] public class PartyDbTests: 
        BaseClassTests<PartyDb, BaseDb<PartyDb>> {
        private DbContextOptions<PartyDb> options;
        private class testClass : PartyDb {
            public testClass(DbContextOptions<PartyDb> o) : base(o) { }
            public ModelBuilder RunOnModelCreating() {
                var set = new ConventionSet();
                var mb = new ModelBuilder(set);
                OnModelCreating(mb);

                return mb;
            }

        }
        protected override PartyDb createObject() {
            options = new DbContextOptionsBuilder<PartyDb>().UseInMemoryDatabase("TestDb").Options;
            return new PartyDb(options);
        }

        [TestMethod] public void InitializeTablesTest() {
            PartyDb.InitializeTables(null);
            var o = new testClass(options);
            var builder = o.RunOnModelCreating();
            PartyDb.InitializeTables(builder);
            testEntity<BodyMetricData>(builder);
            testEntity<BodyMetricTypeData>(builder);
            testEntity<PartySummaryData>(builder);
            testEntity<PersonEthnicityData>(builder);
            testEntity<PartyCapabilityData>(builder);
            testEntity<CountryData>(builder);
            testEntity<PartyContactUsageData>(builder);
            testEntity<PartyContactData>(builder);
            testEntity<DeviceRegistrationData>(builder);
            testEntity<RegisteredIdentifierData>(builder);
            testEntity<RegistrationAuthorityData>(builder);
            testEntity<PartyNameData>(builder);
            testEntity<PartyNameUseData>(builder);
            testEntity<NamePrefixData>(builder);
            testEntity<NameSuffixData>(builder);
            testEntity<PreferenceOptionData>(builder);
            testEntity<PreferenceData>(builder);
            testEntity<AuthenticationData>(builder);
            testEntity<PartySignatureData>(builder);
            testEntity<SignedEntityTypeData>(builder);
            testEntity<PartyData>(builder);
        }

        [TestMethod] public void BodyMetricsTest() => isNullable<DbSet<BodyMetricData>>();
        [TestMethod] public void BodyMetricTypesTest() => isNullable<DbSet<BodyMetricTypeData>>();
        [TestMethod] public void PartySummariesTest() => isNullable<DbSet<PartySummaryData>>();
        [TestMethod] public void PersonEthnicitiesTest() => isNullable<DbSet<PersonEthnicityData>>();
        [TestMethod] public void PartyCapabilitiesTest() => isNullable<DbSet<PartyCapabilityData>>();
        [TestMethod] public void CountriesTest() => isNullable<DbSet<CountryData>>();
        [TestMethod] public void PartyContactUsagesTest() => isNullable<DbSet<PartyContactUsageData>>();
        [TestMethod] public void PartyContactsTest() => isNullable<DbSet<PartyContactData>>();
        [TestMethod] public void TelecomDeviceRegistrationsTest() => isNullable<DbSet<DeviceRegistrationData>>();
        [TestMethod] public void IdentifiersTest() => isNullable<DbSet<RegisteredIdentifierData>>();
        [TestMethod] public void RegistrationAuthoritiesTest() => isNullable<DbSet<RegistrationAuthorityData>>();
        [TestMethod] public void PartyNamesTest() => isNullable<DbSet<PartyNameData>>();
        [TestMethod] public void PersonNamePrefixesTest() => isNullable<DbSet<NamePrefixData>>();
        [TestMethod] public void PersonNameSuffixesTest() => isNullable<DbSet<NameSuffixData>>();
        [TestMethod] public void PartyNameUsesTest() => isNullable<DbSet<PartyNameUseData>>();
        [TestMethod] public void PreferenceOptionsTest() => isNullable<DbSet<PreferenceOptionData>>();
        [TestMethod] public void PreferencesTest() => isNullable<DbSet<PreferenceData>>();
        [TestMethod] public void PartyAuthenticationsTest() => isNullable<DbSet<AuthenticationData>>();
        [TestMethod] public void PartySignaturesTest() => isNullable<DbSet<PartySignatureData>>();
        [TestMethod] public void SignedEntityTypesTest() => isNullable<DbSet<SignedEntityTypeData>>();
        [TestMethod] public void PartiesTest() => isNullable<DbSet<PartyData>>();

        protected static void testKey<T>(IMutableEntityType entity, params Expression<Func<T, object>>[] values) {
            var key = entity.FindPrimaryKey();
            if (values is null) Assert.IsNull(key);
            else
                foreach (var v in values) {
                    var name = GetMember.Name(v);
                    Assert.IsNotNull(key.Properties.FirstOrDefault(x => x.Name == name));
                }
        }

        protected static void testEntity<T>(ModelBuilder b, params Expression<Func<T, object>>[] values) {
            var name = typeof(T).FullName ?? string.Empty;
            var entity = b.Model.FindEntityType(name);
            Assert.IsNotNull(entity, name);
            testKey(entity, values);
        }
    }
}
