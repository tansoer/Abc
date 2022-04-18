using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;

namespace Abc.Infra.Roles {
    public sealed class ResponsibilitiesRepo
        :EntityRepo<Responsibility, ResponsibilityData>,
             IResponsibilitiesRepo {
        public ResponsibilitiesRepo(RoleDb c = null)
            : base(c, c?.Responsibilities) { }
    }
}
