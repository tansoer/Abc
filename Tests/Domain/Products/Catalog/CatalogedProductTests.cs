using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Catalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Catalog {

    [TestClass] public class CatalogedProductTests :SealedTests<CatalogedProduct, Entity<CatalogedProductData>> {

        protected override CatalogedProduct createObject() =>
            new(GetRandom.ObjectOf<CatalogedProductData>());

        [TestMethod] public void CatalogEntryIdTest() => isReadOnly(obj.Data.CatalogEntryId);

        [TestMethod] public void ProductTypeIdTest() => isReadOnly(obj.Data.ProductTypeId);

        [TestMethod] public async Task CatalogEntryTest() {

            await testItemAsync<CatalogEntryData, CatalogEntry, ICatalogEntriesRepo>(
                obj.CatalogEntryId, () => obj.CatalogEntry.Data, d => new CatalogEntry(d));
        }

        [TestMethod] public async Task ProductTypeTest() {
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                obj.ProductTypeId, () => obj.ProductType.Data, ProductTypeFactory.Create);
        }

    }

}