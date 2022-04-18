using Abc.Data.Common;
using Abc.Domain.Common;

namespace Abc.Domain.Parties.Signatures {

    public sealed class SignedEntity<TEntity, TData> : Entity<TData>, ISignedEntity
        where TData : EntityBaseData, new()
        where TEntity : Entity<TData>, new() {

        public SignedEntity() : this(null) { }

        public SignedEntity(TData d) : base(d) { }

        public TEntity Entity => SignedEntityFactory.Create(Data) as TEntity;

    }

}