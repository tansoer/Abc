using Abc.Data.Orders;
using Abc.Domain.Orders.Terms;
using Abc.Facade.Common;

namespace Abc.Facade.Orders {
    public sealed class TermsAndConditionsViewFactory
        :AbstractViewFactory<TermsAndConditionsData, TermsAndConditions, TermsAndConditionsView> {
        protected internal override TermsAndConditions toObject(TermsAndConditionsData d) =>
            new(d);
    }
}