using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Processes {

    [TestClass]
    public class ActionTypesRepoTests :ProcessReposTests<ActionTypesRepo, ActionType,
        ActionTypeData> {

        protected override Type getBaseClass() => typeof(EntityRepo<ActionType, ActionTypeData>);

        protected override ActionTypesRepo getObject(ProcessDb c) => new ActionTypesRepo(c);

        protected override DbSet<ActionTypeData> getSet(ProcessDb c) => c.ActionTypes;
    }
}