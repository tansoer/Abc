using Abc.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Exceptions {
    [TestClass]
    public class PartyExceptionTests :AbstractTests<PartyException, DomainException> {
        private class testClass :PartyException { }
        protected override PartyException createObject() => new testClass();
    }
}