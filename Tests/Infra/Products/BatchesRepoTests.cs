using System;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class BatchesRepoTests : ProductRepoTests<BatchesRepo,
        Batch, BatchData> {

        protected override Type getBaseClass() => typeof(EntityRepo<Batch, BatchData>);

        protected override BatchesRepo getObject(ProductDb c) => new BatchesRepo(c);

        protected override DbSet<BatchData> getSet(ProductDb c) => c.Batches;

    }

}