using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Identifiers;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class RegistrationAuthoritiesRepoTests : PartyRepoTests<RegistrationAuthoritiesRepo,
        RegistrationAuthority, RegistrationAuthorityData> {
        protected override Type getBaseClass()
            => typeof(EntityRepo<RegistrationAuthority, RegistrationAuthorityData>);
        protected override RegistrationAuthoritiesRepo getObject(PartyDb c) =>
            new RegistrationAuthoritiesRepo(c);
        protected override DbSet<RegistrationAuthorityData> getSet(PartyDb c) => c.RegistrationAuthorities;
    }
}