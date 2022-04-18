using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass] public class PreferenceViewTests : SealedTests<PreferenceView, PartyAttributeView> {
        [TestMethod] public void UnitIdTest() => isProperty<string>("Unit");
        [TestMethod] public void PreferenceOptionIdTest() => isProperty<string>("Option");
        [TestMethod] public void PreferenceTypeIdTest() => isProperty<string>("Preference");
        [TestMethod] public void PartyRoleIdTest() => isProperty<string>("Party Role");
        [TestMethod] public void WeightTest() => isProperty<double>();
    }
}
