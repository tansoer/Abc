using Abc.Data.Classifiers;

namespace Abc.Infra.Classifiers.Initializers {
    public static class PartyRegisteredIdentifierTypesInitializer {
        public static void Initialize(ClassifierDb c)
            => ClassifierInitializer.Initialize(c, ClassifierType.RegisteredIdentifier,
                "Passport", "Identity card", "VAT number", 
                "Domain name", "Stock Exchange Symbol", 
                "Registered identifier");
    }

}
