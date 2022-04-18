using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class PackageComponentDataTests : SealedTests<PackageComponentData, EntityBaseData> {
        [TestMethod] public void ProductTypeIdTest() => isNullable<string>();
        [TestMethod] public void PackageTypeIdTest() => isNullable<string>();

    }

}
