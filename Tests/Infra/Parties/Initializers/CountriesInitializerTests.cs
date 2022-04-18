using Abc.Infra.Parties;
using Abc.Infra.Parties.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Parties.Initializers {
    [TestClass] public class CountriesInitializerTests :DbInitializerTests<PartyDb> {
        public CountriesInitializerTests() {
            type = typeof(CountriesInitializer);
            db = new PartyDb(options);
            CountriesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void CountriesTest() 
            => isTrue(135 <= db.Countries.Count(), $"{db.Countries.Count()} < {135}" );
    }
}