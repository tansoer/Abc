using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class CatalogedProductDataTests : SealedTests<CatalogedProductData, EntityBaseData> {
        [TestMethod] public void CatalogEntryIdTest() => isNullable<string>();
        [TestMethod] public void ProductTypeIdTest() => isNullable<string>();

    }

}