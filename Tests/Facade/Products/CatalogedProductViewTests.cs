using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class CatalogedProductViewTests : SealedTests<CatalogedProductView, EntityBaseView> {
        [TestMethod] public void CatalogEntryIdTest() => isNullableProperty<string>("Catalog Entry");
        [TestMethod] public void ProductTypeIdTest() => isNullableProperty<string>("Product Type");
    }
}