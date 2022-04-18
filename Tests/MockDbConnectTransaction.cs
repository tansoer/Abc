using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Abc.Tests {
    public class MockDbConnectTransaction :IDbContextTransaction {
        public Mock Mock { get; }
        public MockDbConnectTransaction(Mock m) => Mock = m;
        public void Dispose() => Mock.Run(nameof(Dispose));
        public async ValueTask DisposeAsync() => await Mock.RunAsync(nameof(DisposeAsync));
        public void Commit() => Mock.Run(nameof(Commit));
        public async Task CommitAsync(CancellationToken t = new()) 
            =>await Mock.RunAsync(nameof(CommitAsync));
        public void Rollback() => Mock.Run(nameof(Rollback));
        public async Task RollbackAsync(CancellationToken t = new()) 
            => await Mock.RunAsync(nameof(RollbackAsync));
        public Guid TransactionId => Mock.Run(nameof(TransactionId), Guid.NewGuid());
    }
}