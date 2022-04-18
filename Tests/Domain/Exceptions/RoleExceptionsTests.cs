using Abc.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Exceptions {
    [TestClass]
    public class RoleExceptionTests :AbstractTests<RoleException, DomainException> {
        private class testClass :RoleException { }
        protected override RoleException createObject() => new testClass();
    }
}