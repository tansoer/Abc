using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Processes {

    [TestClass] public class ActionApproversRepoTests :ProcessReposTests<ActionApproversRepo, ActionApprover,
        ActionApproverData> {
        protected override Type getBaseClass() => typeof(EntityRepo<ActionApprover, ActionApproverData>);
        protected override ActionApproversRepo getObject(ProcessDb c) => new ActionApproversRepo(c);
        protected override DbSet<ActionApproverData> getSet(ProcessDb c) => c.ActionApprovers;
    }
}