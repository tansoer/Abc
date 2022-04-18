using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class CatalogEntryViewTests : SealedTests<CatalogEntryView, EntityBaseView> {

        [TestMethod] public void CatalogIdTest() => isNullableProperty<string>("Catalog");

        [TestMethod] public void CategoryIdTest() => isNullableProperty<string>("Category");
    }
}