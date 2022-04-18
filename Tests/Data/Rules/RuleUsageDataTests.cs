using Abc.Data.Common;
using Abc.Data.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Rules {

    [TestClass]
    public class RuleUsageDataTests : SealedTests<RuleUsageData, EntityBaseData> {

        [TestMethod] public void RuleIdTest() => isNullable<string>();

        [TestMethod] public void RuleSetIdTest() => isNullable<string>();

    }

}