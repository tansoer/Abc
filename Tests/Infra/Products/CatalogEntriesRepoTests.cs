using System;
using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class CatalogEntriesRepoTests : ProductRepoTests<CatalogEntriesRepo,
        CatalogEntry, CatalogEntryData> {

        protected override Type getBaseClass() => typeof(EntityRepo<CatalogEntry, CatalogEntryData>);

        protected override CatalogEntriesRepo getObject(ProductDb c) => new CatalogEntriesRepo(c);

        protected override DbSet<CatalogEntryData> getSet(ProductDb c) => c.CatalogEntries;

    }

}