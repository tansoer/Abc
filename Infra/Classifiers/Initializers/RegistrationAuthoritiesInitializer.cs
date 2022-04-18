using Abc.Data.Classifiers;

namespace Abc.Infra.Classifiers.Initializers {
    public static class RegistrationAuthoritiesInitializer {
        public static void Initialize(ClassifierDb c)
            => ClassifierInitializer.Initialize(c, ClassifierType.RegistrationAuthority,
                            "Estonian Republic", "European Union", "NASDAQ");
    }

}
