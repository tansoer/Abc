using Abc.Infra.Common;
using Abc.Tests.Data;
using Abc.Tests.Domain;

namespace Abc.Tests.Infra
{
    public sealed class TestRepo : EntityRepo<TestObject, TestData>,
        ITestRepo {
        public TestRepo(TestDb t = null) : base(t, t?.TestData) { }
        protected internal override TestObject toDomainObject(TestData data) => new (data);
    }
}