using Abc.Data.Rules;
using Abc.Facade.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Rules {

    [TestClass]
    public class RuleViewTests : SealedTests<RuleView, CommonRuleIdView> {

        [TestMethod] public void RuleKindTest() => isProperty<RuleKind>("Rule Kind");

    }

}
