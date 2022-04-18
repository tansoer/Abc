using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {
    [TestClass]
    public class UnitRulesViewTests : SealedTests<UnitRulesView, UnitAttributeView> {
        [TestMethod] public void FromBaseUnitRuleIdTest() => isNullableProperty<string>(("From base units rule"));
        [TestMethod] public void ToBaseUnitRuleIdTest() => isNullableProperty<string>("To base units rule");
        [TestMethod] public void ToFormulaTest() => isNullable<string>();
        [TestMethod] public void FromFormulaTest() => isNullable<string>();
    }
}
