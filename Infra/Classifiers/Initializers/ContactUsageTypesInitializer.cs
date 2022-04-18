using Abc.Data.Classifiers;

namespace Abc.Infra.Classifiers.Initializers {
    public static class ContactUsageTypesInitializer {
        public static void Initialize(ClassifierDb c)
            => ClassifierInitializer.Initialize(c, ClassifierType.ContactUsage,
                            "Emergency", "Home", "Work", "School", "Private", "Official" );
    }

}
