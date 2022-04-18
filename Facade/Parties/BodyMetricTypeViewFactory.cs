using Abc.Aids.Methods;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {

    public sealed class BodyMetricTypeViewFactory : AbstractViewFactory<BodyMetricTypeData, BodyMetricType, BodyMetricTypeView> {
        protected internal override BodyMetricType toObject(BodyMetricTypeData d) => new(d);
    }

}
