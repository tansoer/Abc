using Abc.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Exceptions {
    [TestClass]
    public class MoneyExceptionTests :AbstractTests<MoneyException, DomainException> {
        private class testClass :MoneyException { }
        protected override MoneyException createObject() => new testClass();
    }
}