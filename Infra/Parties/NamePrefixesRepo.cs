using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Infra.Common;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Infra.Parties {

    public sealed class NamePrefixesRepo : EntityRepo<NamePrefix, NamePrefixData>, INamePrefixesRepo {
        public NamePrefixesRepo(PartyDb c = null) : base(c, c?.PersonNamePrefixes) { }
        public int NextIndex(string masterId) => list(masterId).Count + 1;
        internal List<NamePrefixData> list(string masterId) => dbSet.Where(x => x.NameId == masterId).ToList();
    }
}

