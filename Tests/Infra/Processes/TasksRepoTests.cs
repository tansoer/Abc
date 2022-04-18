using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Abc.Tests.Infra.Processes
{
    [TestClass] public class TasksRepoTests :ProcessReposTests<TasksRepo, ITask,
        TaskData> {
        protected override Type getBaseClass() => typeof(PagedRepo<ITask, TaskData>);
        protected override TasksRepo getObject(ProcessDb c) => new(c);
        protected override DbSet<TaskData> getSet(ProcessDb c) => c.Tasks;
    }
}