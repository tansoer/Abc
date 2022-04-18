using Abc.Data.Processes;

namespace Abc.Infra.Processes.Initializers {
    public class TaskTypesInitializer :DbInitializer {
        private static ProcessDb db;

        public static void Initialize(ProcessDb c) {
            if (c is null) return;
            db = c;
            initializeTaskTypes();
        }

        private static void initializeTaskTypes() {
            static void add(string id, string name, string code, string thread, string prev, string next)
                => addItem(new TaskTypeData { Id = id, Name = name, Code = code, ThreadTypeId = thread, PreviousElementId = prev, NextElementId = next }, db);

            add("ta1", "Getting instructions from the call centre", "ta1", "th1", null, "ta2");
            add("ta2", "Arriving to the location", "ta2", "th1", "ta1", "ta3");
            add("ta3", "Examination", "ta3", "th1", "ta2", "ta4");
            add("ta4", "Onsite treatment", "ta4", "th1", "ta3", "ta5");
            add("ta5", "Deciding to take patient to the hospital", "ta5", "th1", "ta4", "ta6");
            add("ta6", "Riding to the emergency medical centre", "ta6", "th1", "ta5", "ta7");
            add("ta7", "Reaching emergency department", "ta7", "th1", "ta6", "ta8");

            add("ta8", "Gathering additional medical information", "ta8", "th2", "ta7", "ta9");
            add("ta9", "Performing essential laboratory tests and monitoring", "ta9", "th2", "ta8", "ta10");
            add("ta10", "Diagnosing", "ta10", "th2", "ta9", "ta11");
            add("ta11", "Creating a treatment plan", "ta11", "th2", "ta10", "ta12");
            add("ta12", "Informing acute care team of specialists", "ta12", "th2", "ta11", "ta13");

            add("ta13", "Conducting tests, analysis", "ta13", "th3", "ta12", "ta14");
            add("ta14", "Giving feedback to specialists", "ta14", "th3", "ta13", "ta15");
            add("ta15", "Offering proper daily care", "ta15", "th3", "ta14", "ta16");
            add("ta16", "Assessing swallowing functions", "ta16", "th3", "ta15", "ta17");

            add("ta17", "Organizing a meeting", "ta17", "th4", "ta16", "ta18");
            add("ta18", "Administering", "ta18", "th4", "ta17", "ta19");
            add("ta19", "Checking out", "ta19", "th4", "ta18", "ta20");
            add("ta20", "Communicating with the stroke team", "ta20", "th4", "ta19", "ta21");
            add("ta21", "Assessing patient's motor abilities", "ta21", "th4", "ta20", "ta22");
            add("ta22", "Conducting physiotherapy", "ta22", "th4", "ta21", null);
        }
    }
}
