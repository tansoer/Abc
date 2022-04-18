using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Processes {
    [TestClass] public class TaskParticipantsRepoTests :ProcessReposTests<TaskParticipantsRepo, TaskParticipant,
        TaskParticipantData> {

        protected override Type getBaseClass() => typeof(EntityRepo<TaskParticipant, TaskParticipantData>);

        protected override TaskParticipantsRepo getObject(ProcessDb c) => new(c);

        protected override DbSet<TaskParticipantData> getSet(ProcessDb c) => c.TaskParticipants;
    }
}