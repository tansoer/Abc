using Abc.Aids.Methods;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Common;

namespace Abc.Facade.Quantities {

    public sealed class MeasureTermViewFactory : AbstractViewFactory<CommonTermData, MeasureTerm, MeasureTermView> {
        protected internal override MeasureTerm toObject(CommonTermData d) => new (d);
    }

}
