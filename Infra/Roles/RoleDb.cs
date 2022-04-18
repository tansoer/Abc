using Abc.Data.Roles;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Roles {

    public class RoleDb :BaseDb<RoleDb> {
        public RoleDb(DbContextOptions<RoleDb> o) : base(o) { }
        public DbSet<AssignedResponsibilityData> AssignedResponsibilities { get; set; }
        public DbSet<RelationshipConstraintData> RelationshipConstraints { get; set; }
        public DbSet<RelationshipConstraintTypeData> RelationshipConstraintTypes { get; set; }
        public DbSet<PartyRelationshipData> Relationships { get; set; }
        public DbSet<PartyRelationshipTypeData> RelationshipTypes { get; set; }
        public DbSet<PartyRoleConstraintData> RoleConstraints { get; set; }
        public DbSet<PartyRoleConstraintTypeData> RoleConstraintTypes { get; set; }
        public DbSet<PartyRoleData> Roles { get; set; }
        public DbSet<PartyRoleTypeData> RoleTypes { get; set; }
        public DbSet<ResponsibilityData> Responsibilities { get; set; }
        public DbSet<ResponsibilityTypeData> ResponsibilityTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder b) {
            base.OnModelCreating(b);
            InitializeTables(b);
        }
        public static void InitializeTables(ModelBuilder b) {
            if (b is null) return;
            var s = "Role";
            toTable<AssignedResponsibilityData>(b, nameof(AssignedResponsibilities), s);
            toTable<RelationshipConstraintData>(b, nameof(RelationshipConstraints), s);
            toTable<RelationshipConstraintTypeData>(b, nameof(RelationshipConstraintTypes), s);
            toTable<PartyRelationshipData>(b, nameof(Relationships), s);
            toTable<PartyRelationshipTypeData>(b, nameof(RelationshipTypes), s);
            toTable<PartyRoleConstraintData>(b, nameof(RoleConstraints), s);
            toTable<PartyRoleConstraintTypeData>(b, nameof(RoleConstraintTypes), s);
            toTable<PartyRoleData>(b, nameof(Roles), s);
            toTable<PartyRoleTypeData>(b, nameof(RoleTypes), s);
            toTable<ResponsibilityData>(b, nameof(Responsibilities), s);
            toTable<ResponsibilityTypeData>(b, nameof(ResponsibilityTypes), s);
        }
    }
}
