using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class PackageContentDataTests : SealedTests<PackageContentData, EntityBaseData> {

        [TestMethod] public void ProductIdTest() => isNullable<string>();
        [TestMethod] public void PackageIdTest() => isNullable<string>();
        [TestMethod] public void ComponentIdTest() => isNullable<string>();

    }

}