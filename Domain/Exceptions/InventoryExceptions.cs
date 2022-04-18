namespace Abc.Domain.Exceptions {
    public abstract class InventoryException: DomainException {
    }
    public sealed class NoInventoryEntryData: InventoryException  {
    }
    public sealed class DuplicatedEntryProduct :InventoryException {
    }
    public sealed class UnknownEntryProduct :InventoryException {
    }
    public sealed class UnspecifiedEntryInventory :InventoryException {
    }
    public sealed class UnspecifiedEntryProduct :InventoryException {
    }
}