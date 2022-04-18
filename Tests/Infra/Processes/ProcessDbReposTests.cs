using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Infra.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Processes {

    [TestClass]
    public class ProcessDbReposTests :TestsHost {
        [TestInitialize] public void TestInitialize() => type = typeof(ProcessDbRepos);
        [DataTestMethod]
        [DataRow(typeof(IProcessTypesRepo))]
        [DataRow(typeof(IThreadsRepo))]
        [DataRow(typeof(IAttributeValuesRepo))]
        [DataRow(typeof(IActionsRepo))]
        [DataRow(typeof(IActionTypesRepo))]
        [DataRow(typeof(IActionApproversRepo))]
        [DataRow(typeof(ITaskTypesRepo))]
        [DataRow(typeof(IOutcomeTypesRepo))]
        [DataRow(typeof(IOutcomeApproversRepo))]
        [DataRow(typeof(ITasksRepo))]
        [DataRow(typeof(IThreadTypesRepo))]
        [DataRow(typeof(IOutcomesRepo))]
        [DataRow(typeof(ITaskParticipantsRepo))]
        public void RegisterTest(Type t) => Assert.IsNotNull(GetRepo.Instance(t));
    }
}