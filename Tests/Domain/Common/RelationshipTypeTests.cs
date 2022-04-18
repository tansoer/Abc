using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Common {

    [TestClass]
    public class RelationshipTypeTests :

        AbstractTests<RelationshipType<ProductRelationshipTypeData>, 
            Entity<ProductRelationshipTypeData>> {
        private class testClass : RelationshipType<ProductRelationshipTypeData> {
            public testClass(ProductRelationshipTypeData d) : base(d) { }
        }
        protected override RelationshipType<ProductRelationshipTypeData> createObject() =>
            new testClass(GetRandom.ObjectOf<ProductRelationshipTypeData>());
        [TestMethod] public void ConsumerTypeIdTest() => isReadOnly(obj.Data.ConsumerTypeId);
        [TestMethod] public void ProviderTypeIdTest() => isReadOnly(obj.Data.ProviderTypeId);
    }
}