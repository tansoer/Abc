using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Processes {

    [TestClass]
    public class TaskTypesRepoTests :ProcessReposTests<TaskTypesRepo, TaskType,
        TaskTypeData> {

        protected override Type getBaseClass() => typeof(EntityRepo<TaskType, TaskTypeData>);

        protected override TaskTypesRepo getObject(ProcessDb c) => new TaskTypesRepo(c);

        protected override DbSet<TaskTypeData> getSet(ProcessDb c) => c.TaskTypes;
    }
}