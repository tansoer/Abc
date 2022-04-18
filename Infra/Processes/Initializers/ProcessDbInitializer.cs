
namespace Abc.Infra.Processes.Initializers {
    public static class ProcessDbInitializer {
        public static void Initialize(ProcessDb db) {
            if (db is null) return;
            ProcessTypesInitializer.Initialize(db);
            ThreadTypesInitializer.Initialize(db);
            TaskTypesInitializer.Initialize(db);
            ActionTypesInitializer.Initialize(db);
            OutcomeTypesInitializer.Initialize(db);
        }
    }
}
