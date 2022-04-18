using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Common;

namespace Abc.Facade.Currencies {

    public sealed class RateTypeViewFactory :
        AbstractViewFactory<RateTypeData, RateType, RateTypeView> {
        protected internal override RateType toObject(RateTypeData d) => new RateType(d);
    }

}