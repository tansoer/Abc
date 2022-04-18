using Abc.Aids.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Linq.Expressions;
namespace Abc.Tests.Infra {

    public abstract class DbTests<TClass, TBaseClass> : BaseClassTests<TClass, TBaseClass>
        where TClass : DbContext {

        protected DbContextOptions<TClass> options;

        protected static void testKey<T>(IMutableEntityType entity, params Expression<Func<T, object>>[] values) {
            var key = entity.FindPrimaryKey();

            if (values is null) Assert.IsNull(key);
            else
                foreach (var v in values) {
                    var name = GetMember.Name(v);
                    Assert.IsNotNull(key.Properties.FirstOrDefault(x => x.Name == name));
                }
        }
        protected static void testEntity<T>(ModelBuilder b, params Expression<Func<T, object>>[] values) {
            var name = typeof(T).FullName ?? string.Empty;
            var entity = b.Model.FindEntityType(name);
            Assert.IsNotNull(entity, name);
            testKey(entity, values);
        }
        protected void hasDbSet<T>(ModelBuilder b, params Expression<Func<T, object>>[] values) where T : class {
            var name = getNameAfter(nameof(hasDbSet));
            isNullableProperty(obj, name, typeof(DbSet<T>));
            testEntity(b, values);
        }
    }
}
