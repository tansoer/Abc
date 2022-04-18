using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass]
    public class PartySummaryDataTests : SealedTests<PartySummaryData, PartyAttributeData> {
        protected override PartySummaryData createObject() => GetRandom.ObjectOf<PartySummaryData>();

        [TestMethod] public void AddressTest() => isNullable<string>();

        [TestMethod] public void PhoneNumberTest() => isNullable<string>();

        [TestMethod] public void EmailAddressTest() => isNullable<string>();
        [TestMethod] public void OrderLineIdTest() => isNullable<string>();
        [TestMethod] public void IsPartyRoleInOrderTest() 
            => isReadOnly(obj.RoleInOrder != PartyRoleInOrder.Unspecified);
        [TestMethod] public void PartyRoleIdTest() => isNullable<string>();
        [TestMethod] public void OrderIdTest() => isNullable<string>();
        [TestMethod] public void PartySummaryRoleClassifierIdTest() => isNullable<string>();
        [TestMethod] public void RoleInOrderTest() => isProperty<PartyRoleInOrder>();
    }
}