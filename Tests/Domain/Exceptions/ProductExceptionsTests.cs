using Abc.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Exceptions {
    [TestClass]
    public class ProductExceptionTests :AbstractTests<ProductException, DomainException> {
        private class testClass :ProductException { }
        protected override ProductException createObject() => new testClass();
    }
}