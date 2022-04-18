using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;

namespace Abc.Infra.Roles {
    public sealed class ResponsibilityTypesRepo
        :EntityRepo<ResponsibilityType, ResponsibilityTypeData>,
             IResponsibilityTypesRepo {
        public ResponsibilityTypesRepo(RoleDb c = null)
            : base(c, c?.ResponsibilityTypes) { }
    }
}
