using Abc.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Exceptions {
    [TestClass]
    public class ProcessExceptionTests :AbstractTests<ProcessException, DomainException> {
        private class testClass :ProcessException { }
        protected override ProcessException createObject() => new testClass();
    }
}