using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class ProductSetContentDataTests :SealedTests<ProductSetContentData, EntityBaseData> {
        [TestMethod] public void ProductTypeIdTest() => isNullable<string>();
        [TestMethod] public void ProductSetIdTest() => isNullable<string>();
    }
}