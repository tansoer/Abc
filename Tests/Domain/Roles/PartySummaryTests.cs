using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Roles;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Roles {
    [TestClass]
    public class PartySummaryTests : SealedTests<PartySummary, BasePartySummary> {

        protected override PartySummary createObject()
            => new (GetRandom.ObjectOf<PartySummaryData>());

    }
}
