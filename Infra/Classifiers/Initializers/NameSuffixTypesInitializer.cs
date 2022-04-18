using Abc.Data.Classifiers;

namespace Abc.Infra.Classifiers.Initializers {
    public static class NameSuffixTypesInitializer {

        public static void Initialize(ClassifierDb c)
            => ClassifierInitializer.Initialize(c, ClassifierType.PersonNameSuffix,
            "Sr.", "Jr.", "2nd", "C3rd", "II", "III", "IV", "V", "VI",
            "Esq.", "CPA", "DC", "DDS", "VM", "JD", "MD", "PhD"
        );
    }
}