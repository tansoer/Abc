using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Preferences;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class PreferenceOptionsRepoTests : PartyRepoTests<PreferenceOptionsRepo,
        PreferenceOption, PreferenceOptionData> {

        protected override Type getBaseClass()
            => typeof(EntityRepo<PreferenceOption, PreferenceOptionData>);

        protected override PreferenceOptionsRepo getObject(PartyDb c) =>
            new PreferenceOptionsRepo(c);

        protected override DbSet<PreferenceOptionData> getSet(PartyDb c) => c.PreferenceOptions;

    }

}