using Abc.Data.Parties;
using Abc.Domain.Roles;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {

    public sealed class PartySummariesRepo : PagedRepo<IPartySummary, PartySummaryData>,
        IPartySummariesRepo {
        public PartySummariesRepo(PartyDb c = null) : base(c, c?.PartySummaries) { }

        protected internal override IPartySummary toDomainObject(PartySummaryData d) => PartySummaryFactory.Create(d);
    }
}


