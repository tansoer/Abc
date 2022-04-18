using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;
using Abc.Infra.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Inventory {
    [TestClass]
    public class ReservationsRepoTests :InventoryRepoTests<ReservationsRepo, Reservation,
        ReservationData> {

        protected override Type getBaseClass() => typeof(EntityRepo<Reservation, ReservationData>);

        protected override ReservationsRepo getObject(InventoryDb c) => new (c);

        protected override DbSet<ReservationData> getSet(InventoryDb c) => c.Reservations;
    }
}