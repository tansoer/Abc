using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Common {
    [TestClass]
    public class RelationshipTests : AbstractTests<Relationship<ProductRelationshipData>, Entity<ProductRelationshipData>> {
        private class testClass : Relationship<ProductRelationshipData> {
            public testClass(ProductRelationshipData d) : base(d) { }
        }
        protected override Relationship<ProductRelationshipData> createObject() 
            => new testClass(GetRandom.ObjectOf<ProductRelationshipData>());
        [TestMethod] public void RelationshipTypeIdTest() => isReadOnly(obj.Data.RelationshipTypeId);
        [TestMethod] public void ConsumerEntityIdTest() => isReadOnly(obj.Data.ConsumerEntityId);
        [TestMethod] public void ProviderEntityIdTest() => isReadOnly(obj.Data.ProviderEntityId);
    }
}
