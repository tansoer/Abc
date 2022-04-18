using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Preferences;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class PreferencesRepoTests :PartyRepoTests<PreferencesRepo, Preference, PreferenceData> {
        protected override Type getBaseClass() => typeof(EntityRepo<Preference, PreferenceData>);
        protected override PreferencesRepo getObject(PartyDb c) => new PreferencesRepo(c);
        protected override DbSet<PreferenceData> getSet(PartyDb c) => c.Preferences;
    }
}