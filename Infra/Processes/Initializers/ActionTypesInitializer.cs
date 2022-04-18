using Abc.Data.Processes;

namespace Abc.Infra.Processes.Initializers {
    public class ActionTypesInitializer :DbInitializer {
        private static ProcessDb db;

        public static void Initialize(ProcessDb c) {
            if (c is null) return;
            db = c;
            initializeActionTypes();
        }

        private static void initializeActionTypes() {
            static void add(string id, string name, string code, string task)
                => addItem(new ActionTypeData { Id = id, Name = name, Code = code, TaskTypeId = task }, db);

            add("a1", "Follow the route given by the call centre", "a1", "ta1");
            add("a2", "Collect data", "a2", "ta2");
            add("a3", "Objective analysis of the patient", "a3", "ta3");
            add("a4", "Consult with a doctor when necessary", "a4", "ta3");
            add("a5", "Stabilizing the patient", "a5", "ta4");
            add("a6", "Transport patient to the emergency vehicle on a stretcher", "a6", "ta5");
            add("a7", "Patient suitable for thrombolysis and is transported to the hospital", "a7", "ta5");
            add("a8", "Patient is transported to hospital where CT-scan is available", "a8", "ta5");
            add("a9", "Patient is treated and observed during the drive", "a9", "ta6");
            add("a10", "Transport the patient to EMO", "a10", "ta7");

            add("a11", "Analyse data given by other specialists", "a11", "ta8");
            add("a12", "View patients medical history", "a12", "ta8");
            add("a13", "Order specific test, analyses to be conducted", "a13", "ta9");
            add("a14", "Determine any generic diseases", "a14", "ta10");
            add("a15", "Exclude other non-vascular diseases", "a15", "ta10");
            add("a16", "Diagnose the specific type of stroke the patient has", "a16", "ta10");
            add("a17", "Prescribing specific treatment", "a17", "ta11");
            add("a18", "Prevention of complications", "a18", "ta11");
            add("a19", "Create a rehabilitation plan", "a19", "ta11");
            add("a20", "Secondary prevention", "a20", "ta11");
            add("a21", "Recommendations on lifestyle changes", "a21", "ta11");
            add("a22", "Prescribe medication", "a22", "ta11");
            add("a23", "Team meeting", "a23", "ta12");

            add("a24", "Monitor blood pressure, swallowing function, etc.", "a24", "ta13");
            add("a25", "Fill and read the card situated next to patients bed, with data about their care", "a25", "ta14");
            add("a26", "Help with primary activities - using bathroom, hygiene, etc.", "a26", "ta15");
            add("a27", "Prevent bed sores and infections", "a27", "ta15");
            add("a28", "Initial assessment", "a28", "ta16");

            add("a29", "Meeting between physician and therapists", "a29", "ta17");
            add("a30", "Have an overall understanding of patients and daily operations", "a30", "ta18");
            add("a31", "Oversee that patient has necessary prescriptions and referrals", "a31", "ta19");
            add("a32", "Documentation of patients health issues", "a32", "ta20");
            add("a33", "Assessing disturbed mobility, muscle weakness, cumbersome movements", "a33", "ta21");
            add("a34", "Conduct exercises", "a34", "ta22");
            add("a35", "Use equipment and exercise tools meant for physiotherapy", "a35", "ta22");
            add("a36", "Give feedback to the patient and exercises to carry on at home", "a36", "ta22");
            add("a37", "Before patient leaves, conduct therapy with patient's family and give instructions", "a37", "ta22");
        }
    }
}
