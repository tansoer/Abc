using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Parties;
using Abc.Domain.Common;

namespace Abc.Domain.Parties.Names {

    public sealed class NamePrefix : Entity<NamePrefixData> {

        public NamePrefix() : this(null) { }
        public NamePrefix(NamePrefixData d) : base(d) { }

        public NamePrefixType Type => item<IClassifiersRepo, IClassifier>(PrefixTypeId) as NamePrefixType;

        public string PrefixTypeId => get(Data?.PrefixTypeId);
    }
}