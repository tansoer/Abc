using Abc.Infra.Parties.Initializers;

namespace Abc.Infra.Classifiers.Initializers {
    public static class ClassifierDbInitializer {
        public static void Initialize(ClassifierDb db) {
            if (db is null) return;
            ContactUsageTypesInitializer.Initialize(db);
            NamePrefixTypesInitializer.Initialize(db);
            NameSuffixTypesInitializer.Initialize(db);
            OrganizationTypesInitializer.Initialize(db);
            RegistrationAuthoritiesInitializer.Initialize(db);
            PartyRegisteredIdentifierTypesInitializer.Initialize(db);
            OrganizationTypesInitializer.Initialize(db);
            TermsAndConditionsTypesInitializer.Initialize(db);
        }
    }
}
