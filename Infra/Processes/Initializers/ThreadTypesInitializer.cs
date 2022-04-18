using Abc.Data.Processes;

namespace Abc.Infra.Processes.Initializers {
    public class ThreadTypesInitializer :DbInitializer {
        private static ProcessDb db;

        public static void Initialize(ProcessDb c) {
            if (c is null) return;
            db = c;
            initializeThreadTypes();
        }

        private static void initializeThreadTypes() {
            static void add(string id, string name, string code, string process)
                => addItem(new ThreadTypeData { Id = id, Name = name, Code = code, ProcessTypeId = process }, db);

            add("th1", "PERH guidelines", "th1", "Stroke care process");
            add("th2", "Emergency Medicine Centre guidelines", "th2", "Stroke care process");
            add("th3", "Neurology Centre guidelines", "th3", "Stroke care process");
            add("th4", "North Estonian Rehabilitation Centre guidelines", "th4", "Stroke care process");
        }
    }
}
