using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Parties.Capabilities;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Domain.Roles {
    public sealed class Responsibility: Entity<ResponsibilityData> {
        public Responsibility() : this(null) { }
        public Responsibility(ResponsibilityData d) : base(d) { }
        public ResponsibilityType Type => item<IResponsibilityTypesRepo, ResponsibilityType>(typeId);
        public bool IsOptional => Data?.IsOptional ?? false;
        internal string typeId => get(Data?.ResponsibilityTypeId);
        internal string roleTypeId => get(Data?.PartyRoleTypeId);
        public bool IsCapable(IReadOnlyList<PartyCapability> c) => c?.Any(x => x.IsCapable(Type)) ?? false;
    }
}
