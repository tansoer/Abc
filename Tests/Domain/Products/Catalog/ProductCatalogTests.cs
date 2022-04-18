using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products.Catalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Catalog {

    [TestClass]
    public class ProductCatalogTests : SealedTests<ProductCatalog, Entity<ProductCatalogData>> {

        protected override ProductCatalog createObject() => new (GetRandom.ObjectOf<ProductCatalogData>());

        [TestMethod]
        public async Task CatalogEntriesTest()
            => await testListAsync<CatalogEntry, CatalogEntryData, ICatalogEntriesRepo>(
                obj, nameof(obj.CatalogEntries),
                x => x.CatalogId = obj.Id,
                d => new CatalogEntry(d));
    }

}