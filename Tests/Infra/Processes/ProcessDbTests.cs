using Abc.Data.Processes;
using Abc.Infra;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Processes {

    [TestClass]
    public class ProcessDbTests :DbTests<ProcessDb, BaseDb<ProcessDb>> {
        private class testClass :ProcessDb {
            public testClass(DbContextOptions<ProcessDb> o) : base(o) { }
            public ModelBuilder RunOnModelCreating() {
                var set = new ConventionSet();
                var mb = new ModelBuilder(set);
                OnModelCreating(mb);
                return mb;
            }
        }
        protected override ProcessDb createObject() {
            options = new DbContextOptionsBuilder<ProcessDb>().UseInMemoryDatabase("TestDb").Options;
            return new ProcessDb(options);
        }
        [TestMethod]
        public void InitializeTablesTest() {
            ProcessDb.InitializeTables(null);
            var o = new testClass(options);
            var builder = o.RunOnModelCreating();
            ProcessDb.InitializeTables(builder);
            testEntity<ProcessData>(builder);
            testEntity<ProcessTypeData>(builder);
            testEntity<ThreadData>(builder);
            testEntity<AttributeValueData>(builder);
            testEntity<ActionData>(builder);
            testEntity<ActionTypeData>(builder);
            testEntity<ActionApproverData>(builder);
            testEntity<ActionTypeData>(builder);
            testEntity<TaskTypeData>(builder);
            testEntity<OutcomeTypeData>(builder);
            testEntity<OutcomeApproverData>(builder);
            testEntity<ThreadTypeData>(builder);
            testEntity<TaskParticipantData>(builder);
        }
        [TestMethod] public void ProcessesTest() => isNullable<DbSet<ProcessData>>();
        [TestMethod] public void ProcessTypesTest() => isNullable<DbSet<ProcessTypeData>>();
        [TestMethod] public void ThreadsTest() => isNullable<DbSet<ThreadData>>();
        [TestMethod] public void AttributeValuesTest() => isNullable<DbSet<AttributeValueData>>();
        [TestMethod] public void ActionsTest() => isNullable<DbSet<ActionData>>();
        [TestMethod] public void ActionTypesTest() => isNullable<DbSet<ActionTypeData>>();
        [TestMethod] public void ActionApproversTest() => isNullable<DbSet<ActionApproverData>>();
        [TestMethod] public void TaskTypesTest() => isNullable<DbSet<TaskTypeData>>();
        [TestMethod] public void OutcomeTypesTest() => isNullable<DbSet<OutcomeTypeData>>();
        [TestMethod] public void OutcomeApproversTest() => isNullable<DbSet<OutcomeApproverData>>();
        [TestMethod] public void TasksTest() => isNullable<DbSet<TaskData>>();
        [TestMethod] public void OutcomesTest() => isNullable<DbSet<OutcomeData>>();
        [TestMethod] public void AttributeTypesTest() => isNullable<DbSet<AttributeTypeData>>();
        [TestMethod] public void ThreadTypesTest() => isNullable<DbSet<ThreadTypeData>>();
        [TestMethod] public void TaskParticipantsTest() => isNullable<DbSet<TaskParticipantData>>();
    }
}