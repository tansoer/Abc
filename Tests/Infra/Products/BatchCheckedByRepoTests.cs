using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class BatchCheckedByRepoTests : ProductRepoTests<BatchCheckedByRepo,
        BatchCheckedBy, BatchCheckedByData> {
        protected override Type getBaseClass() => typeof(EntityRepo<BatchCheckedBy, BatchCheckedByData>);
        protected override BatchCheckedByRepo getObject(ProductDb c) => new BatchCheckedByRepo(c);
        protected override DbSet<BatchCheckedByData> getSet(ProductDb c) => c.BatchCheckedByParties;
    }
}