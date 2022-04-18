using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass] public class MassTests : TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(Mass);
        [TestMethod] public void MeasureTest() => isInstanceOfType(Mass.Measure, typeof(UnitInfo));
        [TestMethod] public void UnitsTest() => areEqual(18, Mass.Units.Count);
    }
}