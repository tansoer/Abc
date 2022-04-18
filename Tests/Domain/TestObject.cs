using Abc.Domain.Common;
using Abc.Tests.Data;

namespace Abc.Tests.Domain {
    public sealed class TestObject : Entity<TestData> {
        public TestObject() : this(null) { }
        public TestObject(TestData d = null) : base(d) { }
    }
}
