using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;

namespace Abc.Facade.Processes {
    public sealed class AttributeValueViewFactory :AbstractViewFactory<
        AttributeValueData, AttributeValue, AttributeValueView> {
        protected internal override AttributeValue toObject(AttributeValueData d)
            => AttributeValuesFactory.Create(d);
    }
}