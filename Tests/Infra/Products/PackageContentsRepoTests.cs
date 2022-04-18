using System;
using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class PackageContentsRepoTests : ProductRepoTests<PackageContentsRepo,
        PackageContent, PackageContentData> {
        protected override Type getBaseClass() => typeof(EntityRepo<PackageContent, PackageContentData>);
        protected override PackageContentsRepo getObject(ProductDb c) => new PackageContentsRepo(c);
        protected override DbSet<PackageContentData> getSet(ProductDb c) => c.PackageContents;
    }
}