
namespace Abc.Tests {
    public abstract class ClassTests<TClass, TBaseClass>: 
        BaseClassTests<TClass, TBaseClass> where TClass : class {
        protected override TClass createObject() => random<TClass>();
    }
}