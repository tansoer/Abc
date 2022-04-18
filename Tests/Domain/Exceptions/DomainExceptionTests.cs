using Abc.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Abc.Tests.Domain.Exceptions {
    [TestClass]
    public class DomainExceptionTests :AbstractTests<DomainException, ApplicationException> {
        
        private class testClass: DomainException{}

        protected override DomainException createObject() => new testClass();
    }
}
