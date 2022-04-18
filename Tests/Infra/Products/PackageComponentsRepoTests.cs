using System;
using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class PackageComponentsRepoTests : ProductRepoTests<PackageComponentsRepo,
        PackageComponent, PackageComponentData> {
        protected override Type getBaseClass() => typeof(EntityRepo<PackageComponent, PackageComponentData>);
        protected override PackageComponentsRepo getObject(ProductDb c) => new PackageComponentsRepo(c);
        protected override DbSet<PackageComponentData> getSet(ProductDb c) => c.PackageComponents;
    }
}