using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Facade.Products;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class CatalogedProductViewFactoryTests : ViewFactoryTests<CatalogedProductViewFactory, CatalogedProductData,
        CatalogedProduct, CatalogedProductView> {
    }
}