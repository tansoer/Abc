using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Data.Common;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Infra.Common;
using Abc.Infra.Quantities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Common {

    [TestClass] public class OrderedRepoTests :OrderedRepoTests<Measure, MeasureData> {
        private class testClass :OrderedRepo<Measure, MeasureData> {
            public testClass(DbContext c, DbSet<MeasureData> s) : base(c, s) { }
            protected internal override Measure toDomainObject(MeasureData d) => MeasureFactory.Create(d);
        }
        protected override OrderedRepo<Measure, MeasureData> createObject() {
            var options = new DbContextOptionsBuilder<QuantityDb>().UseInMemoryDatabase("TestDb").Options;
            var c = new QuantityDb(options);
            return new testClass(c, c.Measures);
        }
    }
    public abstract class OrderedRepoTests<TDomain, TData>: AbstractTests<OrderedRepo<TDomain, TData>,
        FilteredRepo<TDomain, TData>>
        where TDomain : IEntity<TData>
        where TData : EntityBaseData, new() {
        [TestMethod] public void SortOrderTest() => isNullable<string>();
        [TestMethod] public void DescendingStringTest() => isReadOnly("_desc");
        [DataTestMethod] [DynamicData(nameof(classProperties), DynamicDataSourceType.Method)]
        [TestMethod] public void ExprTest(string s) => testCreateExpression(s);
        [DataTestMethod] [DynamicData(nameof(classProperties), DynamicDataSourceType.Method)]
        public void ExprDescTest(string s) => testCreateExpression(s, s + obj.DescendingString);
        [DataTestMethod] [DynamicData(nameof(randomStrings), DynamicDataSourceType.Method)]
        public void NullExprTest(string name) {
            obj.SortOrder = name;
            var lambda = obj.getExpression();
            Assert.IsNull(lambda);
        }
        private void testCreateExpression(string expected, string name = null) {
            name ??= expected;
            obj.SortOrder = name;
            var lambda = obj.getExpression();
            Assert.IsInstanceOfType(lambda, typeof(Expression<Func<TData, object>>));
            Assert.IsTrue(lambda.ToString().Contains(expected));
        }
        [TestMethod] public void LambdaExpressionTest() {
            var name = GetMember.Name<TData>(x => x.ValidFrom);
            var property = typeof(TData).GetProperty(name);
            var lambda = OrderedRepo<TDomain, TData>.getExpression(property);
            Assert.IsNotNull(lambda);
            Assert.IsInstanceOfType(lambda, typeof(Expression<Func<TData, object>>));
            Assert.IsTrue(lambda.ToString().Contains(name));
        }
        void findPropertyTest (PropertyInfo expected, string name) {
            obj.SortOrder = name;
            Assert.AreEqual(expected, obj.getProperty());
        }
        [DataTestMethod] [DynamicData(nameof(randomStrings), DynamicDataSourceType.Method)]
        public void FindNoPropertyTest(string name) => findPropertyTest(null, name);
        [DataTestMethod] [DynamicData(nameof(classProperties), DynamicDataSourceType.Method)]
        public void FindPropertyTest(string name) {
            var expected = typeof(TData).GetProperty(name);
            obj.SortOrder = name;
            Assert.AreEqual(expected, obj.getProperty());
            obj.SortOrder += obj.DescendingString;
            Assert.AreEqual(expected, obj.getProperty());
        }
        [DataTestMethod] [DynamicData(nameof(randomStrings), DynamicDataSourceType.Method)]
        public void GetNameTest(string sortOrder) 
            => getNameTest(sortOrder, sortOrder ?? string.Empty);
        [DataTestMethod] [DynamicData(nameof(randomStrings), DynamicDataSourceType.Method)]
        public void GetNameDescTest(string sortOrder) 
            => getNameTest(sortOrder + obj.DescendingString, sortOrder ?? string.Empty);
        private void getNameTest(string sortOrder, string expected) {
            obj.SortOrder = sortOrder;
            Assert.AreEqual(expected, obj.getName());
        }
        [TestMethod] public void AddOrderByTest() {
            void test(IQueryable<TData> d, Expression<Func<TData, object>> e, string expected) {
                obj.SortOrder = rndStr;
                var set = obj.addOrderBy(d, e);
                var s = set.Expression.ToString();
                Assert.AreEqual(s, sortExprStr($"OrderBy({expected})"));
            }
            Assert.IsNull(obj.addOrderBy(null, null));
            IQueryable<TData> data = obj.dbSet;
            Assert.AreEqual(data, obj.addOrderBy(data, null));
            test(data, x => x.Id, "x => x.Id");
            test(data, x => x.Code, "x => x.Code");
            test(data, x => x.Name, "x => x.Name");
            test(data, x => x.Details, "x => x.Details");
            test(data, x => x.ValidFrom, "x => Convert(x.ValidFrom, Object)");
            test(data, x => x.ValidTo, "x => Convert(x.ValidTo, Object)");
        }
        [TestMethod] public void AddOrderByDescendingTest() {
            void test(IQueryable<TData> d, Expression<Func<TData, object>> e, string expected) {
                obj.SortOrder = rndStr + obj.DescendingString;
                var set = obj.addOrderBy(d, e);
                var s = set.Expression.ToString();
                Assert.AreEqual(s, sortExprStr($"OrderByDescending({expected})"));
            }

            Assert.IsNull(obj.addOrderBy(null, null));
            IQueryable<TData> data = obj.dbSet;
            Assert.AreEqual(data, obj.addOrderBy(data, null));
            test(data, x => x.Id, "x => x.Id");
            test(data, x => x.Code, "x => x.Code");
            test(data, x => x.Name, "x => x.Name");
            test(data, x => x.Details, "x => x.Details");
            test(data, x => x.ValidFrom, "x => Convert(x.ValidFrom, Object)");
            test(data, x => x.ValidTo, "x => Convert(x.ValidTo, Object)");
        }
        [TestMethod] public void SortNullTest() {
            Assert.IsNull(obj.addOrderBy(null));
            IQueryable<TData> data = obj.dbSet;
            obj.SortOrder = null;
            Assert.AreEqual(data, obj.addOrderBy(data));
        }
        [DataTestMethod] [DynamicData(nameof(classProperties), DynamicDataSourceType.Method)]
        public void SortTest(string sortOrder) => testSorting(sortOrder, "OrderBy");
        [DataTestMethod] [DynamicData(nameof(classProperties), DynamicDataSourceType.Method)]
        public void SortDescendingTest(string sortOrder) => testSorting(sortOrder + obj.DescendingString, "OrderByDescending");
        private static string sortExprStr(string orderBy, string sortOrder)
            => "[Microsoft.EntityFrameworkCore.Query.QueryRootExpression]." +
                $"{orderBy}(x => Convert(x.{sortOrder}, Object))";
        private static string sortExprStr(string orderBy)
            => $"[Microsoft.EntityFrameworkCore.Query.QueryRootExpression].{orderBy}";
        private void testSorting(string sortOrder, string orderBy) {
            IQueryable<TData> data = obj.dbSet;
            obj.SortOrder = sortOrder;
            sortOrder = sortOrder.Replace(obj.DescendingString, string.Empty);
            var set = obj.addOrderBy(data);
            var actual = set.Expression.ToString();
            var expected = sortExprStr(orderBy, sortOrder);
            Assert.AreEqual(actual, expected);
        }
        [DataTestMethod] [DynamicData(nameof(randomStrings), DynamicDataSourceType.Method)]
        public void IsDescendingTest(string sortOrder) {
            obj.SortOrder = sortOrder;
            Assert.AreEqual(false, obj.isDescending());
            obj.SortOrder += obj.DescendingString;
            Assert.AreEqual(true, obj.isDescending());
        }
        private static IEnumerable<object[]> classProperties() {
            yield return new object[] { GetMember.Name<TData>(x => x.Id) };
            yield return new object[] { GetMember.Name<TData>(x => x.Code) };
            yield return new object[] { GetMember.Name<TData>(x => x.Name) };
            yield return new object[] { GetMember.Name<TData>(x => x.Details) };
            yield return new object[] { GetMember.Name<TData>(x => x.ValidFrom) };
            yield return new object[] { GetMember.Name<TData>(x => x.ValidTo) };
        }
        private static IEnumerable<object[]> randomStrings() {
            yield return new object[] { null };
            yield return new object[] { string.Empty };
            yield return new object[] { rndStr };
        }
    }
}
