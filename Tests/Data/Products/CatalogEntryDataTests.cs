using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class CatalogEntryDataTests : SealedTests<CatalogEntryData, EntityBaseData> {
        [TestMethod] public void CatalogIdTest() => isNullable<string>();
        [TestMethod] public void CategoryIdTest() => isNullable<string>();

    }

}