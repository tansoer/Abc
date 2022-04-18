using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Parties;
using Abc.Domain.Roles;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Roles {
    [TestClass] public class PartySummaryFactoryTests: TestsBase {
        [TestInitialize] public void TestInitialize()
            => type = typeof(PartySummaryFactory);

        [TestMethod] public void CreateTest() {
            var d = random<PartySummaryData>();
            d.RoleInOrder = PartyRoleInOrder.Unspecified;
            isInstanceOfType<PartySummary>(PartySummaryFactory.Create(d));
        }
        [TestMethod]
        public void CreateRoleInOrderTest() {
            var d = random<PartySummaryData>();
            while (d.RoleInOrder == PartyRoleInOrder.Unspecified) {
                d.RoleInOrder = GetRandom.EnumOf<PartyRoleInOrder>();
            }
            isInstanceOfType<IPartyInOrder>(PartySummaryFactory.Create(d));
        }
    }
}
