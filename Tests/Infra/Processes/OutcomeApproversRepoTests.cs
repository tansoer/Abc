using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Processes {
    [TestClass]
    public class OutcomeApproversRepoTests :ProcessReposTests<OutcomeApproversRepo, OutcomeApprover,
        OutcomeApproverData> {

        protected override Type getBaseClass() => typeof(EntityRepo<OutcomeApprover, OutcomeApproverData>);

        protected override OutcomeApproversRepo getObject(ProcessDb c) => new(c);

        protected override DbSet<OutcomeApproverData> getSet(ProcessDb c) => c.OutcomeApprovers;
    }
}
