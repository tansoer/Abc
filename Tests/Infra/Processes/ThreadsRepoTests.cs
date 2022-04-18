using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Processes {

    [TestClass]
    public class ThreadsRepoTests :ProcessReposTests<ThreadsRepo, Thread,
        ThreadData> {

        protected override Type getBaseClass() => typeof(EntityRepo<Thread, ThreadData>);

        protected override ThreadsRepo getObject(ProcessDb c) => new ThreadsRepo(c);

        protected override DbSet<ThreadData> getSet(ProcessDb c) => c.Threads;
    }
}