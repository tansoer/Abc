using Abc.Aids.Random;
using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Common {

    [TestClass]
    public class EntityTypeDataTests : AbstractTests<EntityTypeData, EntityBaseData> {

        private class testClass : EntityTypeData { }
        protected override EntityTypeData createObject() => GetRandom.ObjectOf<testClass>();
        [TestMethod] public void BaseTypeIdTest() => isNullable<string>();
    }
}