using Abc.Domain.Roles;
using Microsoft.Extensions.DependencyInjection;

namespace Abc.Infra.Roles {

    public static class RoleDbRepos {

        public static void Register(IServiceCollection c) {
            c.AddTransient<IAssignedResponsibilitiesRepo, AssignedResponsibilitiesRepo>();
            c.AddTransient<IPartyRelationshipsRepo, PartyRelationshipsRepo>();
            c.AddTransient<IPartyRelationshipTypesRepo, PartyRelationshipTypesRepo>();
            c.AddTransient<IPartyRoleConstraintsRepo, PartyRoleConstraintsRepo>();
            c.AddTransient<IPartyRoleConstraintTypesRepo, PartyRoleConstraintTypesRepo>();
            c.AddTransient<IPartyRolesRepo, PartyRolesRepo>();
            c.AddTransient<IPartyRoleTypesRepo, PartyRoleTypesRepo>();
            c.AddTransient<IRelationshipConstraintsRepo, RelationshipConstraintsRepo>();
            c.AddTransient<IRelationshipConstraintTypesRepo, RelationshipConstraintTypesRepo>();
            c.AddTransient<IResponsibilitiesRepo, ResponsibilitiesRepo>();
            c.AddTransient<IResponsibilityTypesRepo, ResponsibilityTypesRepo>();
        }
    }
}
