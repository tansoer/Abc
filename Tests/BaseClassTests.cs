using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests {
    public abstract class BaseClassTests<TClass, TBaseClass> : TestsHost where TClass: class{
        protected TClass obj {
            get => objUnderTests as TClass; 
            set => objUnderTests = value; 
        }
        protected abstract TClass createObject();
        protected virtual Type getBaseClass() => typeof(TBaseClass);
        [TestInitialize] public virtual void TestInitialize() {
            type = typeof(TClass);
            obj = createObject();
        }
        [TestMethod] public void CanCreateTest() => Assert.IsNotNull(createObject());
        [TestMethod] public void IsInheritedTest() => areEqual(getBaseClass(), type.BaseType);
    }
}