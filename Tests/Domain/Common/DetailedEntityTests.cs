using Abc.Data.Quantities;
using Abc.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Common {
    [TestClass]
    public class DetailedEntityTests
        :AbstractTests<DetailedEntity<MeasureData>, BaseEntity<MeasureData>> {
        private class testClass :DetailedEntity<MeasureData> {
            public testClass(MeasureData d = null) : base(d) { }
        }
        protected override DetailedEntity<MeasureData> createObject() => new testClass(random<MeasureData>());
        [TestMethod] public void DetailsTest() => isReadOnly(obj.Data.Details);
    }

}
