using Abc.Aids.Reflection;
using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass] public class AttributeValueTypeTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(AttributeValueType);

        [TestMethod] public void CountTest()
            => areEqual(3, GetEnum.Count<AttributeValueType>());

        [TestMethod] public void UnspecifiedTest() =>
            areEqual(0, (int)AttributeValueType.Unspecified);

        [TestMethod] public void AttributeValueTest() =>
            areEqual(1, (int)AttributeValueType.AttributeValue);

        [TestMethod] public void PossibleValueTest() =>
            areEqual(2, (int)AttributeValueType.PossibleValue);
    }
}
