using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
namespace Abc.Tests {
    public sealed class MockDbContext: DbContext {
        public Mock Mock { get; }
        public MockDbContext() { Mock = new Mock(); }
        public MockDbContext([NotNull] DbContextOptions o): this() { }
        public override DbContextId ContextId => new ();
        public override IModel Model => null;
        public override ChangeTracker ChangeTracker => null;
        public override DatabaseFacade Database => new MockDatabaseFacade(this);
        public override EntityEntry Add([NotNull] object entity) => null;
        public override EntityEntry<TEntity> Add<TEntity>([NotNull] TEntity entity)
            where TEntity : class => null;
        public override async ValueTask<EntityEntry> AddAsync([NotNull] object entity,
            CancellationToken cancellationToken = default)
            => await Mock.RunAsync(nameof(AddAsync), (EntityEntry)null);
        public async override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>([NotNull] TEntity entity,
            CancellationToken cancellationToken = default) where TEntity : class 
            => await Mock.RunAsync(nameof(AddAsync), (EntityEntry<TEntity>)null);
        public override void AddRange([NotNull] IEnumerable<object> entities) { }
        public override void AddRange([NotNull] params object[] entities){}
        public async override Task AddRangeAsync(
            [NotNull] IEnumerable<object> entities,
            CancellationToken cancellationToken = default) {
            await Task.CompletedTask;}
        public async override Task AddRangeAsync([NotNull] params object[] entities) {
            await Task.CompletedTask;}
        public override EntityEntry<TEntity> Attach<TEntity>([NotNull] TEntity entity)
            where TEntity : class => null;
        public override EntityEntry Attach([NotNull] object entity) => null;
        public override void AttachRange([NotNull] params object[] entities){}
        public override void AttachRange([NotNull] IEnumerable<object> entities){}
        public override void Dispose(){}
        public async override ValueTask DisposeAsync() { await Task.CompletedTask;}
        public override EntityEntry Entry([NotNull] object entity) => null;
        public override EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity)
            where TEntity : class => null;
        [EditorBrowsable(EditorBrowsableState.Never)] public override bool Equals(object obj) => false;
        public override TEntity Find<TEntity>( params object[] keyValues)
            where TEntity : class => null;
        public override object Find([NotNull] Type entityType,
            params object[] keyValues) => null;
        public async override ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues,
            CancellationToken cancellationToken) where TEntity : class
            => await Mock.RunAsync(nameof(FindAsync), (TEntity)null);
        public async override ValueTask<object> FindAsync([NotNull] Type entityType,
            object[] keyValues, CancellationToken cancellationToken)
            => await Mock.RunAsync(nameof(FindAsync), (object) null);
        public async override ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
            => await Mock.RunAsync(nameof(FindAsync), (TEntity)null);
        public async override ValueTask<object> FindAsync([NotNull] Type entityType, params object[] keyValues)
            => await Mock.RunAsync(nameof(FindAsync), (object)null);
        public override IQueryable<TResult> FromExpression<TResult>(
            [NotNull] Expression<Func<IQueryable<TResult>>> expression) => null;
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => 0;
        public override EntityEntry Remove([NotNull] object entity) => null;
        public override EntityEntry<TEntity> Remove<TEntity>([NotNull] TEntity entity)
            where TEntity : class => null;
        public override void RemoveRange([NotNull] params object[] entities) { }
        public override void RemoveRange([NotNull] IEnumerable<object> entities){}
        public override int SaveChanges(bool acceptAllChangesOnSuccess) => 0;
        public override int SaveChanges() => Mock.Run(nameof(SaveChanges), 0);
        public async override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
            => await Mock.RunAsync(nameof(SaveChangesAsync), 0);
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await Mock.RunAsync(nameof(SaveChangesAsync), 0);
        public override DbSet<TEntity> Set<TEntity>([NotNull] string name)
            where TEntity : class => null;
        public override DbSet<TEntity> Set<TEntity>() where TEntity : class => null;
        [EditorBrowsable(EditorBrowsableState.Never)] public override string ToString() => null;
        public override EntityEntry Update([NotNull] object entity) => null;
        public override EntityEntry<TEntity> Update<TEntity>([NotNull] TEntity entity)
            where TEntity : class => null;
        public override void UpdateRange([NotNull] params object[] entities){}
        public override void UpdateRange([NotNull] IEnumerable<object> entities){}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}
        protected override void OnModelCreating(ModelBuilder modelBuilder){}
    }
}
