using Abc.Data.Parties;
using Abc.Data.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Rules {


    [TestClass]
    public class RuleOverrideDataTests : SealedTests<RuleOverrideData, PartySignatureBaseData> {

        [TestMethod] public void RuleIdTest() => isNullable<string>();

        [TestMethod] public void RuleSetIdTest() => isNullable<string>();

        [TestMethod] public void RuleContextIdTest() => isNullable<string>();

        [TestMethod] public void VariableIdTest() => isNullable<string>();

    }

}
