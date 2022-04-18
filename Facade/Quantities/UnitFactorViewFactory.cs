using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Common;

namespace Abc.Facade.Quantities {

    public sealed class UnitFactorViewFactory : AbstractViewFactory<UnitFactorData, UnitFactor, UnitFactorView> {
        protected internal override UnitFactor toObject(UnitFactorData d) => new(d);
    }

}
