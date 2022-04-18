using Abc.Facade.Common;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {

    [TestClass]
    public class UnitFactorViewTests : SealedTests<UnitFactorView, UnitAttributeView> {
        [TestMethod] public void UnitIdTest() => isNullable<string>();
        [TestMethod] public void SystemOfUnitsIdTest() => isNullable<string>();
        [TestMethod] public void FactorTest() => isProperty<double>();
    }
}
