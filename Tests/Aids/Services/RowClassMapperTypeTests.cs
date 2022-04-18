using Abc.Aids.Reflection;
using Abc.Aids.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Services {

    [TestClass]
    public class RowClassMapperTypeTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(RowClassMapperType);

        [TestMethod]
        public void CountTest()
            => Assert.AreEqual(4, GetEnum.Count<RowClassMapperType>());

        [TestMethod]
        public void ValueTest() =>
            Assert.AreEqual(0, (int) RowClassMapperType.Value);

        [TestMethod]
        public void ColumnNameTest() =>
            Assert.AreEqual(1, (int) RowClassMapperType.ColumnName);

        [TestMethod]
        public void ColumnIndexTest() =>
            Assert.AreEqual(2, (int) RowClassMapperType.ColumnIndex);

        [TestMethod]
        public void UnspecifiedTest() =>
            Assert.AreEqual(9, (int) RowClassMapperType.Unspecified);

    }
}
