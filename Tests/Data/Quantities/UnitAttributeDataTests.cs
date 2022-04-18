using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Data.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Quantities {
    [TestClass] public class UnitAttributeDataTests : 
        AbstractTests<UnitAttributeData, BaseData> {
        private class testClass : UnitAttributeData { }
        protected override UnitAttributeData createObject() => GetRandom.ObjectOf<testClass>();
        [TestMethod] public void SystemOfUnitsIdTest()  => isNullable<string>();
        [TestMethod] public void UnitIdTest() => isNullable<string>();

    }
}
