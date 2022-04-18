using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {
    [TestClass] public class PreferenceDataTests : SealedTests<PreferenceData, PartyAttributeData> {
        [TestMethod] public void UnitIdTest() => isNullable<string>();
        [TestMethod] public void PreferenceOptionIdTest() => isNullable<string>();
        [TestMethod] public void PreferenceTypeIdTest() => isNullable<string>();
        [TestMethod] public void PartyRoleIdTest() => isNullable<string>();
        [TestMethod] public void WeightTest() => isProperty<double>();
    }
}