
using Abc.Data.Inventory;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Inventory {
    [TestClass]
    public class RestockPolicyTests: SealedTests<RestockPolicy, Entity<RestockPolicyData>> {}
}
