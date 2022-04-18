using Abc.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Abc.Infra {
    public abstract class BaseDb<TDb>: DbContext where TDb: DbContext{
        protected BaseDb(DbContextOptions<TDb> o): base(o) {  }
        protected static EntityTypeBuilder<TData> toTable<TData>([NotNull] ModelBuilder b, [NotNull]string name, [NotNull]string schema) 
            where TData : BaseData => b?.Entity<TData>()?.ToTable(name, schema);

        protected static void toTableWithDecimalCol<TData>(
            [NotNull] ModelBuilder b, [NotNull] string name, [NotNull] string schema, 
            Expression<Func<TData, decimal>> e)
            where TData : BaseData 
            => toTable<TData>(b, name, schema).Property(e).HasColumnType("decimal(16,4)");
        protected static void toTableWithOwnsOne<TData, TRelated> (
            [NotNull] ModelBuilder b, [NotNull] string name, [NotNull] string schema,
            Expression<Func<TData, TRelated>> e)
            where TData : BaseData
            where TRelated : class
            => toTable<TData>(b, name, schema).OwnsOne(e);
    }
}
