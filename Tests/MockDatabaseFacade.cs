using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
namespace Abc.Tests {
    public class MockDatabaseFacade :DatabaseFacade {
        public Mock Mock{ get; }
        public MockDatabaseFacade([NotNull] MockDbContext c) :base(c) 
            => Mock = c.Mock;
        public override IDbContextTransaction CurrentTransaction 
            => Mock.Run(nameof(CurrentTransaction), new MockDbConnectTransaction(Mock));
        public override bool AutoTransactionsEnabled { get; set; }
        public override string ProviderName => null;
        public override IDbContextTransaction BeginTransaction()
            => Mock.Run(nameof(BeginTransaction), new MockDbConnectTransaction(Mock));
        public async override Task<IDbContextTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken = default)
            => await Mock.RunAsync(nameof(BeginTransactionAsync), new MockDbConnectTransaction(Mock));
        public override bool CanConnect() => false;
        public async override Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
            => await Mock.RunAsync(nameof(CanConnectAsync), true);

        public override void CommitTransaction() => Mock.Run(nameof(CommitTransaction)); 
        public async override Task CommitTransactionAsync(CancellationToken cancellationToken = default)
            => await Mock.RunAsync(nameof(CommitTransactionAsync));

        public override IExecutionStrategy CreateExecutionStrategy() => null;
        public override bool EnsureCreated() => false;
        public async override Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken = default) {
            await Task.CompletedTask;
            return default;
        }
        public override bool EnsureDeleted() => false;
        public async override Task<bool> EnsureDeletedAsync(CancellationToken cancellationToken = default) {
            await Task.CompletedTask;
            return default;
        }
        [EditorBrowsable(EditorBrowsableState.Never)] public override bool Equals(object obj) => false;
        [EditorBrowsable(EditorBrowsableState.Never)] public override int GetHashCode() => 0;
        public override void RollbackTransaction() { }
        public async override Task RollbackTransactionAsync(CancellationToken cancellationToken = default) {
            await Task.CompletedTask;
        }
        [EditorBrowsable(EditorBrowsableState.Never)] public override string ToString() => null;
    }
}
