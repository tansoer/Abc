using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Preferences;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Preferences {

    [TestClass]
    public class PreferenceOptionTests : SealedTests<PreferenceOption, Entity<PreferenceOptionData>> {
        protected override PreferenceOption createObject()
            => new PreferenceOption(GetRandom.ObjectOf<PreferenceOptionData>());
        [TestMethod] public void typeIdTest() => isReadOnly(obj.Data.PreferenceTypeId);
    }
}