using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class PartyContactUsageViewTests :SealedTests<PartyContactUsageView
        , PartyAttributeView> {
        [TestMethod] public void CodeTest() => isProperty<string>("Contact usage");
        [TestMethod] public void DetailsTest() => isProperty<string>("Comments");
        [TestMethod] public void NameTest() => isProperty<string>("Contact");
    }
}
