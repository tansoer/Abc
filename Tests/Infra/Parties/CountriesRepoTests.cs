using System;
using Abc.Data.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class CountriesRepoTests : PartyRepoTests<CountriesRepo, Country,
        CountryData> {

        protected override Type getBaseClass()
            => typeof(EntityRepo<Country, CountryData>);

        protected override CountriesRepo getObject(PartyDb c) =>
            new CountriesRepo(c);

        protected override DbSet<CountryData> getSet(PartyDb c) => c.Countries;

    }

}