using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests {

    public abstract class AbstractTests<TClass, TBaseClass> : BaseClassTests<TClass, TBaseClass> where TClass: class {

        [TestMethod] public void IsAbstract() => Assert.IsTrue(type.IsAbstract);
        
        protected void isAbstractMethod(string name) {
            var i = type.GetMethod(name);
            Assert.IsNotNull(i);
            Assert.IsTrue(i.IsAbstract);
        }

        protected void isAbstract(string name) {
            var i = type.GetProperty(name);
            Assert.IsNotNull(i);
            Assert.IsTrue(i.CanRead);
            Assert.IsTrue(i.CanWrite);
            Assert.IsTrue(isAbstract(i.GetGetMethod()));
            Assert.IsTrue(isAbstract(i.GetSetMethod()));
        }
        protected void isAbstractReadOnly(string name) {
            var i = type.GetProperty(name);
            Assert.IsNotNull(i);
            Assert.IsTrue(i.CanRead);
            Assert.IsFalse(i.CanWrite);
            Assert.IsTrue(isAbstract(i.GetGetMethod()));
        }
        private static bool isAbstract(MethodInfo i) => i?.IsAbstract ?? false;

        protected void isAbstract() {
            var n = getNameAfter(nameof(isAbstract));
            isAbstract(n);
        }
        protected void isAbstractReadOnly() {
            var n = getNameAfter(nameof(isAbstractReadOnly));
            isAbstractReadOnly(n);
        }

    }
}