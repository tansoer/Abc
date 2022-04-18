using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class SignedEntityTypesRepoTests : PartyRepoTests<SignedEntityTypesRepo,
        SignedEntityType, SignedEntityTypeData> {

        protected override Type getBaseClass()
            => typeof(EntityRepo<SignedEntityType, SignedEntityTypeData>);

        protected override SignedEntityTypesRepo getObject(PartyDb c) =>
            new SignedEntityTypesRepo(c);

        protected override DbSet<SignedEntityTypeData> getSet(PartyDb c) => c.SignedEntityTypes;

    }

}