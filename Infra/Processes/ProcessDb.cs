using Abc.Data.Common;
using Abc.Data.Processes;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Processes {
    public class ProcessDb :BaseDb<ProcessDb> {

        public DbSet<ProcessData> Processes { get; set; }
        public DbSet<ProcessTypeData> ProcessTypes { get; set; }
        public DbSet<ThreadData> Threads { get; set; }
        public DbSet<AttributeValueData> AttributeValues { get; set; }
        public DbSet<ActionData> Actions { get; set; }
        public DbSet<ActionTypeData> ActionTypes { get; set; }
        public DbSet<ActionApproverData> ActionApprovers { get; set; }
        public DbSet<TaskTypeData> TaskTypes { get; set; }
        public DbSet<OutcomeTypeData> OutcomeTypes { get; set; }
        public DbSet<OutcomeApproverData> OutcomeApprovers { get; set; }
        public DbSet<TaskData> Tasks { get; set; }
        public DbSet<OutcomeData> Outcomes { get; set; }
        public DbSet<AttributeTypeData> AttributeTypes { get; set; }
        public DbSet<ThreadTypeData> ThreadTypes { get; set; }
        public DbSet<TaskParticipantData> TaskParticipants { get; set; }

        public ProcessDb(DbContextOptions<ProcessDb> o)
            : base(o) { }

        protected override void OnModelCreating(ModelBuilder b) {
            base.OnModelCreating(b);
            InitializeTables(b);
        }
        public static void InitializeTables(ModelBuilder b) {
            if (b is null) return;
            const string s = "Process";
            toTable<ProcessData>(b, nameof(Processes), s);
            toTable<ProcessTypeData>(b, nameof(ProcessTypes), s);
            toTable<ThreadData>(b, nameof(Threads), s);
            toTableWithOwnsOne<AttributeValueData, ValueData>(b, nameof(AttributeValues), s, x => x.Value);
            toTable<ActionData>(b, nameof(Actions), s);
            toTable<ActionTypeData>(b, nameof(ActionTypes), s);
            toTable<ActionApproverData>(b, nameof(ActionApprovers), s);
            toTable<TaskTypeData>(b, nameof(TaskTypes), s);
            toTable<TaskData>(b, nameof(Tasks), s);
            toTable<OutcomeTypeData>(b, nameof(OutcomeTypes), s);
            toTable<OutcomeData>(b, nameof(Outcomes), s);
            toTable<OutcomeApproverData>(b, nameof(OutcomeApprovers), s);
            toTable<AttributeTypeData>(b, nameof(AttributeTypes), s);
            toTable<ThreadTypeData>(b, nameof(ThreadTypes), s);
            toTable<TaskParticipantData>(b, nameof(TaskParticipants), s);
        }
    }
}