using Abc.Data.Parties;
using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Roles
{
    [TestClass]
    public class AssignedResponsibilityTests :SealedTests<AssignedResponsibility, Entity<AssignedResponsibilityData>> {

        [TestMethod]
        public async Task ResponsibilityTest()
            => await testItemAsync<ResponsibilityData, Responsibility, IResponsibilitiesRepo>(
                obj.responsibilityId, () => obj.Responsibility.Data, d => new Responsibility(d));

        [TestMethod]
        public async Task AssignedByTest()
            => await testItemAsync<PartySignatureData, PartySignature, IPartySignaturesRepo>(
                obj.partySignatureId, () => obj.AssignedBy.Data, d => new PartySignature(d));
    }
}