using Abc.Facade.Common;
using Abc.Tests.Data;
using Abc.Tests.Domain;

namespace Abc.Tests.Facade
{
    public sealed class TestViewFactory : AbstractViewFactory<TestData, TestObject, TestView> {
        protected internal override TestObject toObject(TestData d) => new (d);
    }
}