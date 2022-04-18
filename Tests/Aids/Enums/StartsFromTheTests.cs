using Abc.Aids.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Enums {

    [TestClass]
    public class StartsFromTheTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(StartsFromThe);

        //[TestMethod] public void CountTest() => Assert.AreEqual(2, GetEnum.Count<StartsFromThe>());

        //[TestMethod] public void BeginningTest() => Assert.AreEqual(0, (int) StartsFromThe.Beginning);

        //[TestMethod] public void EndTest() => Assert.AreEqual(9, (int) StartsFromThe.End);

    }

}