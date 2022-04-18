using Abc.Data.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Quantities {

    [TestClass]
    public class UnitFactorDataTests : SealedTests<UnitFactorData, UnitAttributeData> {
        
        [TestMethod] public void FactorTest() => isProperty<double>();

    }

}