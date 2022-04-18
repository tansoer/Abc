using Abc.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Exceptions {
    [TestClass]
    public class InventoryExceptionTests :AbstractTests<InventoryException, DomainException> {
        private class testClass: InventoryException{}
        protected override InventoryException createObject() => new testClass();
    }
    [TestClass]
    public class NoInventoryEntryDataTests: SealedTests< NoInventoryEntryData, InventoryException>  {
    }
    [TestClass]
    public class DuplicatedEntryProductTests: SealedTests<DuplicatedEntryProduct, InventoryException> {
    }
    [TestClass]
    public class UnknownEntryProductTests :SealedTests<UnknownEntryProduct, InventoryException> {
    }
    [TestClass]
    public class UnspecifiedEntryInventoryTests :SealedTests<UnspecifiedEntryInventory, InventoryException> {
    }
    [TestClass]
    public class UnspecifiedEntryProductTests :SealedTests<UnspecifiedEntryProduct, InventoryException> {
    }
}