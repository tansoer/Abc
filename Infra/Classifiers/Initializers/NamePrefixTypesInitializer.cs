using Abc.Data.Classifiers;

namespace Abc.Infra.Classifiers.Initializers {
    public static class NamePrefixTypesInitializer {

        public static void Initialize(ClassifierDb c)
            => ClassifierInitializer.Initialize(c, ClassifierType.PersonNamePrefix,
            "Mr", "Miss", "Mrs", "Ms",
            "Sir", "Lady", "Dame", "Lord",
            "Dr", "Prof");

    }
}
