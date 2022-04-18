using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Preferences;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class PreferenceViewFactoryTests :
        SealedTests<PreferenceViewFactory,
            AbstractViewFactory<PreferenceData, Preference, PreferenceView>> {

        [TestMethod]
        public void CreateTest() {
            var d = GetRandom.ObjectOf<PreferenceData>();
            var o = new Preference(d);
            var v = new PreferenceViewFactory().Create(o);
            allPropertiesAreEqual(v, o.Data);
        }

        [TestMethod]
        public void CreateObjectTest() {
            var v = GetRandom.ObjectOf<PreferenceView>();
            var o = new PreferenceViewFactory().Create(v);
            allPropertiesAreEqual(v, o.Data);
        }
    }
}
