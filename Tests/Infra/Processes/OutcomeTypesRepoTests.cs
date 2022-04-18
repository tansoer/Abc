using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Processes {
    [TestClass]
    public class OutcomeTypesRepoTests :ProcessReposTests<OutcomeTypesRepo, OutcomeType,
        OutcomeTypeData> {

        protected override Type getBaseClass() => typeof(EntityRepo<OutcomeType, OutcomeTypeData>);

        protected override OutcomeTypesRepo getObject(ProcessDb c) => new(c);

        protected override DbSet<OutcomeTypeData> getSet(ProcessDb c) => c.OutcomeTypes;
    }
}