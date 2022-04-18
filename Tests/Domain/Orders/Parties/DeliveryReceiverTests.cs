using Abc.Data.Parties;
using Abc.Domain.Orders.Parties;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Orders.Parties {

    [TestClass]
    public class DeliveryReceiverTests :AbstractTests<DeliveryReceiver, PartyInOrder> {
        private class testClass :DeliveryReceiver {
            public testClass() : this(null) { }
            public testClass(PartySummaryData d) : base(d) { }
        }
        protected override DeliveryReceiver createObject() => new testClass(random<PartySummaryData>());
    }
}