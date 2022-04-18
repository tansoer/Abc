using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Abc.Tests.Infra.Processes
{
    [TestClass] public class OutcomesRepoTests :ProcessReposTests<OutcomesRepo, Outcome,
        OutcomeData> {
        protected override Type getBaseClass() => typeof(EntityRepo<Outcome, OutcomeData>);
        protected override OutcomesRepo getObject(ProcessDb c) => new(c);
        protected override DbSet<OutcomeData> getSet(ProcessDb c) => c.Outcomes;
    }
}