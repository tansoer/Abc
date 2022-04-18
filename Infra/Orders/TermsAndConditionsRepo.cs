using Abc.Data.Orders;
using Abc.Domain.Orders.Terms;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {
    public sealed class TermsAndConditionsRepo :EntityRepo<TermsAndConditions,
        TermsAndConditionsData>, ITermsAndConditionsRepo {
        public TermsAndConditionsRepo(OrderDb c = null) : base(c, c?.TermsAndConditions) { }
    }
}