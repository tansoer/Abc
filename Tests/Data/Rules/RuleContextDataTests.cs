using Abc.Data.Common;
using Abc.Data.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Rules {

    [TestClass]
    public class RuleContextDataTests : SealedTests<RuleContextData, EntityBaseData> {

        [TestMethod] public void RuleSetIdTest() => isNullable<string>();

        [TestMethod] public void RuleIdTest() => isNullable<string>();
    }

}
