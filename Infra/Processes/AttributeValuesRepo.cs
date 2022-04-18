using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;

namespace Abc.Infra.Processes {
    public sealed class AttributeValuesRepo
        :PagedRepo<AttributeValue, AttributeValueData>, IAttributeValuesRepo {
        public AttributeValuesRepo(ProcessDb c = null) : base(c, c?.AttributeValues) { }

        protected internal override AttributeValue toDomainObject(AttributeValueData d)
            => AttributeValuesFactory.Create(d);
    }
}