using Abc.Data.Parties;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Infra.Parties.Initializers {
    public static class SignedEntityTypesInitializer {
        private static List<string> signedEntities => new List<string> {
            "Opportunity", "Contact", "Lead", "Account", "Contract", "Invoice",
            "Order", "Quote", "User"
        };

        public static void Initialize(PartyDb c) {
            if (c is null) return;
            var set = c.SignedEntityTypes;
            if (set.FirstOrDefault() != null) return;
            var l = signedEntities.Select(
                e => new SignedEntityTypeData {
                    Id = e, Name = e, Code = e
                }).ToList();

            set.AddRange(l);
            c.SaveChanges();
        }
    }
}
