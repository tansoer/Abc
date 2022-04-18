using System.Reflection;
using Abc.Domain.Common;

namespace Abc.Domain.Parties.Signatures {

    public sealed class GetSignedEntity {

        private readonly SignedEntityType entityType;

        public GetSignedEntity() : this(null) { }
        public GetSignedEntity(SignedEntityType t) => entityType = t;

        public IEntity ById(string id) {
            var assembly = getAssembly();
            var type = assembly.GetType(entityType.Name);
            var r = GetRepo.Instance(type);
            return r?.Get(id);
        }

        internal Assembly getAssembly() => GetType().Assembly;

    }

}