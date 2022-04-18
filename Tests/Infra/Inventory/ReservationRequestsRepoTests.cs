using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;
using Abc.Infra.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Inventory {

    [TestClass]
    public class ReservationRequestsRepoTests :InventoryRepoTests<ReservationRequestsRepo, ReservationRequest,
        ReservationRequestData> {

        protected override Type getBaseClass() => typeof(PagedRepo<ReservationRequest, ReservationRequestData>);

        protected override ReservationRequestsRepo getObject(InventoryDb c) => new (c);

        protected override DbSet<ReservationRequestData> getSet(InventoryDb c) => c.ReservationRequests;
    }
}