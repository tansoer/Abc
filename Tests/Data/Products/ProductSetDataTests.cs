using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class ProductSetDataTests : SealedTests<ProductSetData, EntityBaseData> {
        [TestMethod] public void PackageTypeIdTest() => isNullable<string>();

    }

}