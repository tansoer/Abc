using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Signatures {
    [TestClass]
    public class PartySignatureTests : SealedTests<PartySignature, BasePartySignature<PartySignatureData>> { }
}
