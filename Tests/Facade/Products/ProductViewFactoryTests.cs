using Microsoft.VisualStudio.TestTools.UnitTesting;
using Abc.Tests.Facade.Common;
using Abc.Data.Products;
using Abc.Facade.Products;
using Abc.Domain.Products;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class ProductViewFactoryTests : ViewFactoryTests<ProductViewFactory, ProductData, IProduct, ProductView> { }
}