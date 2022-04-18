using Abc.Domain.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Capabilities;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Parties.Identifiers;
using Abc.Domain.Parties.Names;
using Abc.Domain.Parties.Preferences;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Roles;
using Microsoft.Extensions.DependencyInjection;

namespace Abc.Infra.Parties {

    public class PartyDbRepos {

        public static void Register(IServiceCollection services) {
            services.AddTransient<IBodyMetricsRepo, BodyMetricsRepo>();
            services.AddTransient<IBodyMetricTypesRepo, BodyMetricTypesRepo>();
            services.AddTransient<IPartySummariesRepo, PartySummariesRepo>();
            services.AddTransient<IPersonEthnicitiesRepo, PersonEthnicitiesRepo>();
            services.AddTransient<IPartyCapabilitiesRepo, PartyCapabilitiesRepo>();
            services.AddTransient<ICountriesRepo, CountriesRepo>();
            services.AddTransient<IPartyContactsRepo, PartyContactsRepo>();
            services.AddTransient<IPartyContactUsagesRepo, PartyContactUsagesRepo>();
            services.AddTransient<IDeviceRegistrationsRepo, DeviceRegistrationsRepo>();
            services.AddTransient<IRegisteredIdentifiersRepo, RegisteredIdentifiersRepo>();
            services.AddTransient<IRegistrationAuthoritiesRepo, RegistrationAuthoritiesRepo>();
            services.AddTransient<IPartyNamesRepo, PartyNamesRepo>();
            services.AddTransient<IPartyNameUsesRepo, PartyNameUsesRepo>();
            services.AddTransient<INameSuffixesRepo, NameSuffixesRepo>();
            services.AddTransient<INamePrefixesRepo, NamePrefixesRepo>();
            services.AddTransient<IPreferenceOptionsRepo, PreferenceOptionsRepo>();
            services.AddTransient<IPreferencesRepo, PreferencesRepo>();
            services.AddTransient<ISignedEntityTypesRepo, SignedEntityTypesRepo>();
            services.AddTransient<IPartySignaturesRepo, PartySignaturesRepo>();
            services.AddTransient<IAuthenticationsRepo, AuthenticationsRepo>();
            services.AddTransient<IPartiesRepo, PartiesRepo>();
        }

    }

}
