using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests {

    public abstract class SealedTests<TClass, TBaseClass> : ClassTests<TClass, TBaseClass> 
        where TClass : class {
        [TestMethod] public void IsSealed() => isTrue(type.IsSealed);
    }
}