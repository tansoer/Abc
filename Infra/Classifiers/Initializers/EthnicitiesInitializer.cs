using Abc.Data.Classifiers;

namespace Abc.Infra.Classifiers.Initializers {
    public static class EthnicitiesInitializer {

        public static void Initialize(ClassifierDb c)
            => ClassifierInitializer.Initialize(c, ClassifierType.PartyEthnicity,
            "European", "Maori", "Pacific Peoples", "Southeast Asian", "Chinese", "Indian", "Other Asian",
            "Middle eastern", "Latin American", "African", "Other ethnicity");
    }
}
