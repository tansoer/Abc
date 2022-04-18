using System.Linq;
using Abc.Data.Parties;

namespace Abc.Infra.Parties.Initializers {

    public static class BodyMetricTypesInitializer {

        public static void Initialize(PartyDb c) {
            if (c is null) return;
            var set = c.BodyMetricTypes;

            if (set.FirstOrDefault() != null) return;
            set.AddRange(
                new BodyMetricTypeData(null, null, null, "Observation"),
                new BodyMetricTypeData(null, null, null, "Diagnosis"),
                new BodyMetricTypeData(null, null, null, "Measurement")
            );
            c.SaveChanges();
        }

    }

}
