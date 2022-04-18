using Abc.Aids.Methods;
using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Methods {

    [TestClass]
    public class CopyTests : TestsBase {

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(Copy);
        }

        [TestMethod]
        public void MembersTest() {
            var x = GetRandom.ObjectOf<SystemOfUnitsData>();
            var y = GetRandom.ObjectOf<SystemOfUnitsView>();
            notEqualProperties(x, y);
            Copy.Members(x, y);
            allPropertiesAreEqual(x, y);
        }
    }
}
