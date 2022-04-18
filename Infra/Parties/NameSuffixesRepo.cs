using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Infra.Common;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Infra.Parties {

    public sealed class NameSuffixesRepo : EntityRepo<NameSuffix, NameSuffixData>, INameSuffixesRepo {
        public NameSuffixesRepo(PartyDb c = null) : base(c, c?.PersonNameSuffixes) { }
        internal List<NameSuffixData> list(string masterId) => dbSet.Where(x => x.NameId == masterId).ToList();
        public int NextIndex(string masterId) => list(masterId).Count + 1;
    }
}