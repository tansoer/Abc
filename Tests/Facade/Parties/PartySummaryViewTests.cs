using Abc.Data.Orders;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class PartySummaryViewTests :SealedTests<PartySummaryView, PartyAttributeView> {
        [TestMethod] public void AddressTest() => isNullableProperty<string>("Address");
        [TestMethod] public void PhoneNumberTest() => isNullableProperty<string>("Phone number");
        [TestMethod] public void EmailAddressTest() => isNullableProperty<string>("Email address");
        [TestMethod] public void PartyRoleIdTest() => isNullableProperty<string>("Party role");
        [TestMethod] public void OrderIdTest() => isNullableProperty<string>("Order");
        [TestMethod] public void OrderLineIdTest() => isNullableProperty<string>("Order line");
        [TestMethod] public void PartySummaryRoleClassifierIdTest() => isNullableProperty<string>("Other role");
        [TestMethod] public void RoleInOrderTest() => isProperty<PartyRoleInOrder>("Role in order");
        [DataTestMethod]
        [DataRow(PartyRoleInOrder.Unspecified, false)]
        [DataRow(PartyRoleInOrder.SalesAgent, true)]
        [DataRow(PartyRoleInOrder.PaymentReceiver, true)]
        [DataRow(PartyRoleInOrder.OrderLineReceiver, true)]
        [DataRow(PartyRoleInOrder.OrderReceiver, true)]
        [DataRow(PartyRoleInOrder.OrderInitiator, true)]
        [DataRow(PartyRoleInOrder.Purchaser, true)]
        [DataRow(PartyRoleInOrder.Vendor, true)]
        public void IsPartyRoleInOrderTest(PartyRoleInOrder r, bool b) {
            obj.RoleInOrder = r;
            areEqual(b, obj.IsPartyRoleInOrder);
        }
    }
}
