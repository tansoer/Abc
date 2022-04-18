using Abc.Data.Common;
using Abc.Data.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Inventory {
    [TestClass]
    public class ReservationReceiverDataTests :SealedTests<ReservationReceiverData, EntityBaseData> {
        [TestMethod] public void ReceiverPartySummaryIdTest() => isNullable<string>();
        [TestMethod] public void ReservationRequestIdTest() => isNullable<string>();
    }
}