using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Processes {
    [TestClass]
    public class ThreadTypesRepoTests :ProcessReposTests<ThreadTypesRepo, ThreadType,
        ThreadTypeData> {

        protected override Type getBaseClass() => typeof(EntityRepo<ThreadType, ThreadTypeData>);

        protected override ThreadTypesRepo getObject(ProcessDb c) => new(c);

        protected override DbSet<ThreadTypeData> getSet(ProcessDb c) => c.ThreadTypes;
    }
}