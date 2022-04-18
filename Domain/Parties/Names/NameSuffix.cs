using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Parties;
using Abc.Domain.Common;

namespace Abc.Domain.Parties.Names {
    public sealed class NameSuffix : Entity<NameSuffixData> {
 
        public NameSuffix() : this(null) { }
        public NameSuffix(NameSuffixData d) : base(d) { }

        public NameSuffixType Type => item<IClassifiersRepo, IClassifier>(typeId) as NameSuffixType;

        internal string typeId => get(Data?.SuffixTypeId);
    }
}