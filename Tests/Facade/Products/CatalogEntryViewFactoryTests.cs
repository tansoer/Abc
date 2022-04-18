using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Facade.Products;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class CatalogEntryViewFactoryTests : ViewFactoryTests<CatalogEntryViewFactory, CatalogEntryData,
        CatalogEntry, CatalogEntryView> { }
}