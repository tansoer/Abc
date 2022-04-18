using Abc.Facade.Common;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {
    [TestClass] public class SystemOfUnitsViewTests : SealedTests<SystemOfUnitsView, QuantityBaseView> {
        [TestMethod] public void CodeTest() => isNullable<string>();
    }
}
