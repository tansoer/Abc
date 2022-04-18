﻿using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Parties;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Orders.Parties {

    [TestClass]
    public class UnspecifiedRoleInOrderTests :SealedTests<UnspecifiedRoleInOrder, PartyInOrder> {
        protected override UnspecifiedRoleInOrder createObject() {
            var d = random<PartySummaryData>();
            d.RoleInOrder = PartyRoleInOrder.Unspecified;
            return PartyInOrderFactory.Create(d) as UnspecifiedRoleInOrder;
        }
    }
}