using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Products {
    [TestClass]
    public sealed class ContainerItemsRepoTests: ProductRepoTests<ContainerItemsRepo, ContainerItem, ContainerItemData> {
        protected override Type getBaseClass() => typeof(EntityRepo<ContainerItem, ContainerItemData>);
        protected override ContainerItemsRepo getObject(ProductDb c) => new ContainerItemsRepo(c);
        protected override DbSet<ContainerItemData> getSet(ProductDb c) => c.ContainerItems;
    }
}
