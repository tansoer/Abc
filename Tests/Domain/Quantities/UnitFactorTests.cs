using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {

    [TestClass]
    public class UnitFactorTests : SealedTests<UnitFactor, UnitAttribute<UnitFactorData>> {

        [TestMethod] public void FactorTest() => isReadOnly(obj.Data.Factor);

    }

}