using Abc.Aids.Enums;
using Abc.Aids.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Reflection {

    [TestClass]
    public class GetEnumTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(GetEnum);

        [TestMethod]
        public void CountTest() {
            Assert.AreEqual(4, GetEnum.Count<IsoGender>());
            Assert.AreEqual(-1, GetEnum.Count<object>());
        }

        [TestMethod]
        public void CountByTypeTest() {
            Assert.AreEqual(4, GetEnum.Count(typeof(IsoGender)));
            Assert.AreEqual(-1, GetEnum.Count(typeof(object)));
        }

        [TestMethod]
        public void ValueByTypeTest() {
            Assert.AreEqual(IsoGender.NotKnown, GetEnum.Value(typeof(IsoGender), 0));
            Assert.AreEqual(IsoGender.Male, GetEnum.Value(typeof(IsoGender), 1));
            Assert.AreEqual(IsoGender.Female, GetEnum.Value(typeof(IsoGender), 2));
            Assert.AreEqual(IsoGender.NotApplicable, GetEnum.Value(typeof(IsoGender), 3));
        }

        [TestMethod]
        public void ValueTest() {
            Assert.AreEqual(IsoGender.NotKnown, GetEnum.Value<IsoGender>(0));
            Assert.AreEqual(IsoGender.Male, GetEnum.Value<IsoGender>(1));
            Assert.AreEqual(IsoGender.Female, GetEnum.Value<IsoGender>(2));
            Assert.AreEqual(IsoGender.NotApplicable, GetEnum.Value<IsoGender>(3));
        }

    }

}



