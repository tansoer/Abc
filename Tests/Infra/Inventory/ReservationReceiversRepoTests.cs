using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;
using Abc.Infra.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Inventory {

    [TestClass]
    public class ReservationReceiversRepoTests :InventoryRepoTests<ReservationReceiversRepo, ReservationReceiver,
        ReservationReceiverData> {

        protected override Type getBaseClass() => typeof(EntityRepo<ReservationReceiver, ReservationReceiverData>);

        protected override ReservationReceiversRepo getObject(InventoryDb c) => new (c);

        protected override DbSet<ReservationReceiverData> getSet(InventoryDb c) => c.ReservationReceivers;

    }
}