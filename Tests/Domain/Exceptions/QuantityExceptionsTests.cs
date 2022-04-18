using Abc.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Exceptions {
    [TestClass]
    public class QuantityExceptionTests :AbstractTests<QuantityException, DomainException> {
        private class testClass :QuantityException { }
        protected override QuantityException createObject() => new testClass();
    }
}