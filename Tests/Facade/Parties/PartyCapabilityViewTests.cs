using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class PartyCapabilityViewTests : SealedTests<PartyCapabilityView, PartyAttributeView> {
        [TestMethod] public void RuleContextIdTest() => isProperty<string>();
    }
}
