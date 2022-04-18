using Abc.Aids.Random;
using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Common {

    [TestClass] public class RelationshipTypeDataTests: 
        AbstractTests<RelationshipTypeData, EntityTypeData> {
        private class testClass : RelationshipTypeData { }
        protected override RelationshipTypeData createObject() =>
            GetRandom.ObjectOf<testClass>();
        [TestMethod] public void ConsumerTypeIdTest() => isNullable<string>();
        [TestMethod] public void ProviderTypeIdTest() => isNullable<string>();
    }
}