using Abc.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Exceptions {
    [TestClass]
    public class RuleExceptionTests :AbstractTests<RuleException, DomainException> {
        private class testClass :RuleException { }
        protected override RuleException createObject() => new testClass();
    }
}