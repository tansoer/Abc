using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;

namespace Abc.Infra.Roles {
    public sealed class AssignedResponsibilitiesRepo
        :EntityRepo<AssignedResponsibility, AssignedResponsibilityData>,
             IAssignedResponsibilitiesRepo {
        public AssignedResponsibilitiesRepo(RoleDb c = null) 
            : base(c, c?.AssignedResponsibilities) { }
    }
}