using Abc.Aids.Methods;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Common;

namespace Abc.Facade.Quantities {

    public sealed class UnitTermViewFactory : AbstractViewFactory<CommonTermData, UnitTerm, UnitTermView> {
        protected internal override UnitTerm toObject(CommonTermData d) => new (d);
    }

}
