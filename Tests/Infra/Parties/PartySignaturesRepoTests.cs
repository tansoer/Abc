using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class PartySignaturesRepoTests : PartyRepoTests<PartySignaturesRepo,
        PartySignature, PartySignatureData> {
        protected override Type getBaseClass()
            => typeof(EntityRepo<PartySignature, PartySignatureData>);
        protected override PartySignaturesRepo getObject(PartyDb c) =>
            new PartySignaturesRepo(c);
        protected override DbSet<PartySignatureData> getSet(PartyDb c) => c.PartySignatures;
    }
}