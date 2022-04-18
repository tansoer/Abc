using Abc.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Exceptions {
    [TestClass]
    public class OrderExceptionTests :AbstractTests<OrderException, DomainException> {
        private class testClass :OrderException { }
        protected override OrderException createObject() => new testClass();
    }
}