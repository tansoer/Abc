using Abc.Facade.Common;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {
    [TestClass]
    public class UnitAttributeViewTests :
        AbstractTests<UnitAttributeView, BaseView> {
        private class testClass :UnitAttributeView { }
        protected override UnitAttributeView createObject() => random<testClass>();
        [TestMethod] public void UnitIdTest() => isNullableProperty<string>("Unit", true);
        [TestMethod] public void SystemOfUnitsIdTest() => isNullableProperty<string>("System of units", true);
    }
}
