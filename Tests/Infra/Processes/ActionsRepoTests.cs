using Abc.Data.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Action = Abc.Domain.Processes.Action;

namespace Abc.Tests.Infra.Processes {

    [TestClass]
    public class ActionsRepoTests :ProcessReposTests<ActionsRepo, Action,
        ActionData> {

        protected override Type getBaseClass() => typeof(EntityRepo<Action, ActionData>);

        protected override ActionsRepo getObject(ProcessDb c) => new ActionsRepo(c);

        protected override DbSet<ActionData> getSet(ProcessDb c) => c.Actions;
    }
}