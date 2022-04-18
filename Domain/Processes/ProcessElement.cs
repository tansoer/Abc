using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Rules;
using System.Collections.Generic;

namespace Abc.Domain.Processes {
    public abstract class ProcessElement<TRepo, TEntity, TData>
        :ProcessElementBase<TRepo, TEntity, TData>, IProcessElement<TEntity>
        where TData : ProcessElementData, new()
        where TEntity : IEntity<TData>
        where TRepo : IRepo<TEntity> {
        protected ProcessElement() : this(null) { }
        protected ProcessElement(TData d) : base(d) { }
        public RuleContext Context => item<IRuleContextsRepo, RuleContext>(contextId);
        internal IReadOnlyList<AttributeValue> attributeValues => list<IAttributeValuesRepo, AttributeValue>(elementId, Id);
        public IReadOnlyList<Attribute> Attributes => list<Attribute, AttributeValue>(attributeValues);
        internal string contextId => get(Data?.RuleContextId);
        internal string elementId => nameOf<AttributeValueData>(x => x.ProcessElementId);
    }

    public interface IProcessElement<TEntity>: IProcessElementBase<TEntity>, IProcessElement {
        public IReadOnlyList<Attribute> Attributes { get; }
    }
    public interface IProcessElement :IProcessElementBase {
        public RuleContext Context { get; }
    }
}
