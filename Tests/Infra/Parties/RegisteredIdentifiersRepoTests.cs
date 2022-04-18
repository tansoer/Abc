using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Identifiers;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class RegisteredIdentifiersRepoTests : PartyRepoTests<RegisteredIdentifiersRepo,
        RegisteredIdentifier, RegisteredIdentifierData> {

        protected override Type getBaseClass()
            => typeof(EntityRepo<RegisteredIdentifier, RegisteredIdentifierData>);

        protected override RegisteredIdentifiersRepo getObject(PartyDb c) => new (c);

        protected override DbSet<RegisteredIdentifierData> getSet(PartyDb c) => c.Identifiers;

    }

}