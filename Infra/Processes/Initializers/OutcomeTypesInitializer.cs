using Abc.Data.Processes;

namespace Abc.Infra.Processes.Initializers {
    internal class OutcomeTypesInitializer :DbInitializer {
        private static ProcessDb db;

        public static void Initialize(ProcessDb c) {
            if (c is null) return;
            db = c;
            initializeOutcomeTypes();
        }

        private static void initializeOutcomeTypes() {
            static void add(string id, string name, string code, string action)
                => addItem(new OutcomeTypeData { Id = id, Name = name, Code = code, ActionTypeId = action }, db);

            add("o1", "Ambulance arrives", "o1", "a1");
            add("o2", "The emergency card has been filled out", "o2", "a2");
            add("o3", "Consciousness, breathing and hemodynamics are analysed", "o3", "a3");
            add("o4", "The doctor has been consulted", "o4", "a4");
            add("o5", "The patient is stabilized", "o5", "a5");
            add("o6", "Patient is prepared for the journey", "o6", "a6");
            add("o7", "Hospital is informed", "o7", "a7");
            add("o8", "Hospital is informed", "o8", "a8");
            add("o9", "Intravenous treatment (IV)", "o9", "a9");
            add("o10", "Admitted to emergency department", "o10", "a10");

            add("o11", "Data is analysed", "o11", "a11");
            add("o12", "Adding personal medical result to the Patient Portal", "o12", "a12");
            add("o13", "Results of laboratory tests", "o13", "a13");
            add("o14", "Generic diseases are determined", "o14", "a14");
            add("o15", "Non-vascular diseases are excluded", "o15", "a15");
            add("o16", "A specific type of stroke has been diagnosed", "o16", "a16");
            add("o17", "Thrombolytic treatment", "o17", "a17");
            add("o18", "Complications are prevented", "o18", "a18");
            add("o19", "A rehabilitation plan has been created", "o19", "a19");
            add("o20", "Secondary prevention has been carried out", "o20", "a20");
            add("o21", "Informed about the reduction of salt and alcohol consumption, weight loss, regular exercise, smoking cessation", "o21", "a21");
            add("o22", "Medications have been prescribed", "o22", "a22");
            add("o23", "The goals have been set", "o23", "a23");

            add("o24", "In case of swallowing functioning issues, the patient is referred to a speech therapist", "o24", "a24");
            add("o25", "It's possible to start daily care", "o25", "a25");
            add("o26", "Inconvenience is prevented", "o26", "a26");
            add("o27", "Complications are prevented", "o27", "a27");
            add("o28", "Patient is referred to the interdisciplinary team", "o28", "a28");

            add("o29", "Short-term goals have been set", "o29", "a29");
            add("o30", "Check-out time is set", "o30", "a30");
            add("o31", "Next appointments for ambulatory therapies are scheduled", "o31", "a31");
            add("o32", "Short-term and long-term goals have been set", "o32", "a32");
            add("o33", "Referral to physiotherapy", "o33", "a33");
            add("o34", "Exercises have been conducted", "o34", "a34");
            add("o35", "Equipment and exercise tools are used", "o35", "a35");
            add("o36", "Feedback has been given", "o36", "a36");
            add("o37", "Instructions have been given", "o37", "a37");
        }
    }
}
