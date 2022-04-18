using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;

namespace Abc.Infra.Quantities {
    public sealed class MeasureTermsRepo : EntityRepo<MeasureTerm, CommonTermData>, IMeasureTermsRepo {
        public MeasureTermsRepo(QuantityDb c = null) : base(c, c?.CommonTerms) { }
    }
}
