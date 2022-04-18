using Abc.Data.Processes;
using System.Collections.Generic;

namespace Abc.Infra.Processes.Initializers {
    public class ProcessTypesInitializer :DbInitializer {
        private static ProcessDb db;

        private static List<string> processTypes => new() {
            "Stroke care process",
        };

        public static void Initialize(ProcessDb c) {
            if (c is null) return;
            db = c;
            initializeProcessTypes();
        }

        private static void initializeProcessTypes() {
            static void add(string c) => addItem(new ProcessTypeData { Id = c, Name = c }, db);
            foreach (var c in processTypes) add(c);
        }

    }
}


