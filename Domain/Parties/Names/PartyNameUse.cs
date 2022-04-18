using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;

namespace Abc.Domain.Parties.Names {

    public sealed class PartyNameUse : Entity<PartyNameUseData> {

        public PartyNameUse() : this(null) { }
        public PartyNameUse(PartyNameUseData d) : base(d) { }

        public NameUsageType Type => item<IClassifiersRepo, IClassifier>(typeId) as NameUsageType;

        internal string typeId => get(Data?.NameUseTypeId);
    }
}