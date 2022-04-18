using Abc.Data.Currencies;
using Abc.Data.Parties;
using Abc.Data.Roles;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Parties {
    public class PartyDb : BaseDb<PartyDb> {

        public DbSet<BodyMetricData> BodyMetrics { get; set; }
        public DbSet<BodyMetricTypeData> BodyMetricTypes { get; set; }
        public DbSet<PartySummaryData> PartySummaries { get; set; }
        public DbSet<PersonEthnicityData> PersonEthnicities { get; set; }
        public DbSet<PartyCapabilityData> PartyCapabilities { get; set; }
        public DbSet<CountryData> Countries { get; set; }
        public DbSet<PartyContactUsageData> PartyContactUsages { get; set; }
        public DbSet<PartyContactData> PartyContacts { get; set; }
        public DbSet<DeviceRegistrationData> TelecomDeviceRegistrations { get; set; }
        public DbSet<RegisteredIdentifierData> Identifiers { get; set; }
        public DbSet<RegistrationAuthorityData> RegistrationAuthorities { get; set; }
        public DbSet<PartyNameData> PartyNames { get; set; }
        public DbSet<NamePrefixData> PersonNamePrefixes { get; set; }
        public DbSet<NameSuffixData> PersonNameSuffixes { get; set; }
        public DbSet<PartyNameUseData> PartyNameUses { get; set; }
        public DbSet<PreferenceOptionData> PreferenceOptions { get; set; }
        public DbSet<PreferenceData> Preferences { get; set; }
        public DbSet<AuthenticationData> PartyAuthentications { get; set; }
        public DbSet<PartySignatureData> PartySignatures { get; set; }
        public DbSet<SignedEntityTypeData> SignedEntityTypes { get; set; }
        public DbSet<PartyData> Parties { get; set; }

        public PartyDb(DbContextOptions<PartyDb> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            InitializeTables(builder);
        }

        public static void InitializeTables(ModelBuilder b) {
            if (b is null) return;
            var s = "Party";
            toTable<BodyMetricData>(b, nameof(BodyMetrics), s);
            toTable<BodyMetricTypeData>(b, nameof(BodyMetricTypes), s);
            toTable<PartySummaryData>(b, nameof(PartySummaries), s);
            toTable<PersonEthnicityData>(b, nameof(PersonEthnicities), s);
            toTable<PartyCapabilityData>(b, nameof(PartyCapabilities), s);
            toTable<CountryData>(b, nameof(Countries), s);
            toTable<PartyContactUsageData>(b, nameof(PartyContactUsages), s);
            toTable<PartyContactData>(b, nameof(PartyContacts), s);
            toTable<DeviceRegistrationData>(b, nameof(TelecomDeviceRegistrations), s);
            toTable<RegisteredIdentifierData>(b, nameof(Identifiers), s);
            toTable<RegistrationAuthorityData>(b, nameof(RegistrationAuthorities), s);
            toTable<PartyNameData>(b, nameof(PartyNames), s);
            toTable<PartyNameUseData>(b, nameof(PartyNameUses), s);
            toTable<NamePrefixData>(b, nameof(PersonNamePrefixes), s);
            toTable<NameSuffixData>(b, nameof(PersonNameSuffixes), s);
            toTable<PreferenceOptionData>(b, nameof(PreferenceOptions), s);
            toTable<PreferenceData>(b, nameof(Preferences), s);
            toTable<AuthenticationData>(b, nameof(PartyAuthentications), s);
            toTable<PartySignatureData>(b, nameof(PartySignatures), s);
            toTable<SignedEntityTypeData>(b, nameof(SignedEntityTypes), s);
            toTable<PartyData>(b, nameof(Parties), s);
        }
    }
}
