using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;

namespace Abc.Infra.Quantities {

    public sealed class UnitTermsRepo : EntityRepo<UnitTerm, CommonTermData>, IUnitTermsRepo {
        public UnitTermsRepo(QuantityDb c = null) : base(c, c?.CommonTerms) { }
    }
}

