using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Processes {

    [TestClass]
    public class ProcessTypesRepoTests :ProcessReposTests<ProcessTypesRepo, ProcessType,
        ProcessTypeData> {

        protected override Type getBaseClass() => typeof(EntityRepo<ProcessType, ProcessTypeData>);

        protected override ProcessTypesRepo getObject(ProcessDb c) => new (c);

        protected override DbSet<ProcessTypeData> getSet(ProcessDb c) => c.ProcessTypes;
    }
}