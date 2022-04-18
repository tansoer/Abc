using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {
    [TestClass] public class PersonEthnicitiesRepoTests : 
        PartyRepoTests<PersonEthnicitiesRepo,
        PersonEthnicity, PersonEthnicityData> {
        protected override Type getBaseClass()
            => typeof(EntityRepo<PersonEthnicity, PersonEthnicityData>);
        protected override PersonEthnicitiesRepo getObject(PartyDb c) =>
            new PersonEthnicitiesRepo(c);
        protected override DbSet<PersonEthnicityData> getSet(PartyDb c) 
            => c.PersonEthnicities;
    }
}