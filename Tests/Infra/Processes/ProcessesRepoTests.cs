using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Abc.Tests.Infra.Processes
{
    [TestClass]
    public class ProcessesRepoTests :ProcessReposTests<ProcessesRepo, Process,
        ProcessData> {

        protected override Type getBaseClass() => typeof(EntityRepo<Process, ProcessData>);

        protected override ProcessesRepo getObject(ProcessDb c) => new (c);

        protected override DbSet<ProcessData> getSet(ProcessDb c) => c.Processes;
    }
}