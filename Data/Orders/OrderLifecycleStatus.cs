
namespace Abc.Data.Orders {
    public enum OrderLifecycleStatus {
        Unspecified = 0,
        Initialized = 1,
        Open = 2,
        Closed = 3,
        Cancelling = 8,
        Cancelled = 9
    }
}
