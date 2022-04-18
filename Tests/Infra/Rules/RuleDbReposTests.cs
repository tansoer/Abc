using System;
using Abc.Domain.Common;
using Abc.Domain.Rules;
using Abc.Infra.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Rules {

    [TestClass]
    public class RuleDbReposTests : TestsHost {

        [TestInitialize] public void TestInitialize() => type = typeof(RuleDbRepos);
        [DataTestMethod]
        [DataRow(typeof(IRuleElementsRepo))]
        [DataRow(typeof(IRulesRepo))]
        [DataRow(typeof(IRuleOverridesRepo))]
        [DataRow(typeof(IRuleSetsRepo))]
        [DataRow(typeof(IRuleContextsRepo))]
        public void RegisterTest(Type t) => Assert.IsNotNull(GetRepo.Instance(t));
    }
}
