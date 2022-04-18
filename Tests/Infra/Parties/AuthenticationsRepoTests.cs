using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class AuthenticationsRepoTests : PartyRepoTests<AuthenticationsRepo, Authentication,
        AuthenticationData> {
        protected override Type getBaseClass()
            => typeof(EntityRepo<Authentication, AuthenticationData>);
        protected override AuthenticationsRepo getObject(PartyDb c) =>
            new AuthenticationsRepo(c);
        protected override DbSet<AuthenticationData> getSet(PartyDb c) => c.PartyAuthentications;
    }
}