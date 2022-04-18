using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Rules;
using System.Collections.Generic;

namespace Abc.Domain.Processes {
    public abstract class ProcessElementType<TRepo, TEntity, TData>
        : ProcessElementBase<TRepo, TEntity,TData>, IProcessElementType<TEntity>
        where TData : ProcessElementTypeData, new()
        where TEntity: IEntity<TData>
        where TRepo: IRepo<TEntity>{
        protected ProcessElementType() : this(null) { }
        protected ProcessElementType(TData d) : base(d) { }
        public IRuleSet Requirements => item<IRuleSetsRepo, IRuleSet>(ruleSetId);
        public virtual TEntity BaseType => item<TRepo, TEntity>(baseTypeId);
        public IReadOnlyList<AttributeType> Attributes => list<IAttributeTypesRepo, AttributeType>(elementTypeId, Id);
        public bool IsSuitable(IProcessElement e) => isSuitable(e?.Context);
        internal bool isSuitable(RuleContext c) => (Requirements?.Evaluate(c) as BooleanVariable)?.Value ?? false;
        internal string ruleSetId => get(Data?.RuleSetId);
        internal string baseTypeId => get(Data?.BaseTypeId);
        internal static string elementTypeId => nameOf<AttributeTypeData>(x => x.ElementTypeId);
    }

    public interface IProcessElementType<TEntity>: IProcessElementBase<TEntity>, IProcessElementType {
        public TEntity BaseType { get; }
    }
    public interface IProcessElementType :IProcessElementBase {
        public IReadOnlyList<AttributeType> Attributes { get; }
        public abstract bool IsSuitable(IProcessElement e);
        public IRuleSet Requirements { get; }
    }
}
