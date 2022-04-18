using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {
    public sealed class BodyMetricTypesRepo : EntityRepo<BodyMetricType, BodyMetricTypeData>,
        IBodyMetricTypesRepo {
        public BodyMetricTypesRepo(PartyDb c = null) : base(c, c?.BodyMetricTypes) { }
    }
}

