using System;
using Abc.Data.Roles;
using Abc.Domain.Parties.Capabilities;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass] public class PartyCapabilitiesRepoTests 
        : PartyRepoTests<PartyCapabilitiesRepo, PartyCapability, PartyCapabilityData> {
        protected override Type getBaseClass()
            => typeof(EntityRepo<PartyCapability, PartyCapabilityData>);
        protected override PartyCapabilitiesRepo getObject(PartyDb c) =>
            new PartyCapabilitiesRepo(c);
        protected override DbSet<PartyCapabilityData> getSet(PartyDb c) => c.PartyCapabilities;
    }
}