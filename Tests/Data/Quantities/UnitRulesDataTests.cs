using Abc.Data.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Quantities {

    [TestClass]
    public class UnitRulesDataTests : SealedTests<UnitRulesData, UnitAttributeData> {
        
        [TestMethod] public void FromBaseUnitRuleIdTest() => isNullable<string>();

        [TestMethod] public void ToBaseUnitRuleIdTest() => isNullable<string>();

        [TestMethod] public void SystemOfUnitsIdTest() => isNullable<string>();

        [TestMethod] public void UnitIdTest() => isNullable<string>();
    }

}