using Abc.Data.Classifiers;
using Abc.Domain.Classifiers.Orders;
using Abc.Domain.Classifiers.Parties;
using Abc.Domain.Classifiers.Processes;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Identifiers;
using Abc.Domain.Parties.Names;
using Abc.Domain.Parties.Preferences;

namespace Abc.Domain.Classifiers {
    public static class ClassifierFactory {

        public static IClassifier Create(ClassifierData d) {
            if (d is null) return error(null);

            return d.ClassifierType switch {
                ClassifierType.Unspecified => error(d),
                ClassifierType.Order => new OrderType(d),
                ClassifierType.PartyCapability => new CapabilityType(d),
                ClassifierType.ContactUsage => new ContactUsageType(d),
                ClassifierType.PersonNamePrefix => new NamePrefixType(d),
                ClassifierType.RegistrationAuthority => new AuthorityType(d),
                ClassifierType.PartyNameUsage => new NameUsageType(d),
                ClassifierType.RegisteredIdentifier => new IdentifierType(d),
                ClassifierType.PartyEthnicity => new Ethnicity(d),
                ClassifierType.PersonNameSuffix => new NameSuffixType(d),
                ClassifierType.Organization => new OrganizationType(d),
                ClassifierType.PartyPreference => new PreferenceType(d),
                ClassifierType.ProcessPriority => new ProcessPriority(d),
                ClassifierType.ActionStatus => new ActionStatus(d),
                ClassifierType.SalesChannel => new SalesChannel(d),
                ClassifierType.TermsAndConditions => new TermsAndConditionsType(d),
                ClassifierType.TaxationType => new TaxationType(d),
                _ => error(d)
            };
        }
        public static ClassifierData Create(IClassifier obj) => obj.Data;
        private static IClassifier error(ClassifierData d) => new ClassifierError(d);
    }
}