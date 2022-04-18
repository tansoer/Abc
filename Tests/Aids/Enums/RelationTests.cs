using Abc.Aids.Enums;
using Abc.Aids.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Enums {

    [TestClass]
    public class RelationTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Relation);

        [TestMethod] public void CountTest() => Assert.AreEqual(8, GetEnum.Count<Relation>());

        [TestMethod] public void IsEqualTest() => Assert.AreEqual(0, (int) Relation.IsEqual);

        [TestMethod] public void IsNotEqualTest() => Assert.AreEqual(1, (int) Relation.IsNotEqual);

        [TestMethod] public void IsLessTest() => Assert.AreEqual(2, (int) Relation.IsLess);

        [TestMethod] public void IsGreaterTest() => Assert.AreEqual(3, (int) Relation.IsGreater);

        [TestMethod] public void IsNotLessTest() => Assert.AreEqual(4, (int) Relation.IsNotLess);

        [TestMethod] public void IsNotGreaterTest() => Assert.AreEqual(5, (int) Relation.IsNotGreater);

        [TestMethod] public void IsMuchLessTest() => Assert.AreEqual(6, (int) Relation.IsMuchLess);

        [TestMethod] public void IsMuchGreaterTest() => Assert.AreEqual(7, (int) Relation.IsMuchGreater);

    }

}