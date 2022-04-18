using Abc.Aids.Random;
using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Common {

    [TestClass] public class RelationshipDataTests:
        AbstractTests<RelationshipData, EntityBaseData> {
        private class testClass :RelationshipData { }
        protected override RelationshipData createObject() =>
            GetRandom.ObjectOf<testClass>();
        [TestMethod] public void RelationshipTypeIdTest() => isNullable<string>();
        [TestMethod] public void ConsumerEntityIdTest() => isNullable<string>();
        [TestMethod] public void ProviderEntityIdTest() => isNullable<string>();
    }
}
