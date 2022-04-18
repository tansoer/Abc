using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class AttributeTypeViewFactory :AbstractViewFactory<AttributeTypeData, AttributeType, AttributeTypeView> {
        protected internal override AttributeType toObject(AttributeTypeData d) => new(d);
    }
}
