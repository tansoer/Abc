using Abc.Aids.Random;
using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Common {
    [TestClass]
    public class EntityBaseDataTests : AbstractTests<EntityBaseData, DetailedData> {
        private class testClass : EntityBaseData { }
        protected override EntityBaseData createObject() => GetRandom.ObjectOf<testClass>();
        [TestMethod] public void NameTest() => isNullable<string>();
        [TestMethod] public void CodeTest() => isNullable<string>();
        [TestMethod] public void DetailsTest() => isNullable<string>();
    }
}