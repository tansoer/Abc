
namespace Abc.Infra.Parties.Initializers {

    public static class PartyDbInitializer {
        public static void Initialize(PartyDb db) {
            if (db is null) return;
            BodyMetricTypesInitializer.Initialize(db);
            SignedEntityTypesInitializer.Initialize(db);
            PartiesInitializer.Initialize(db);
            CountriesInitializer.Initialize(db);
        }
    }
}
