using Abc.Aids.Classes;
using Abc.Aids.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Abc.Tests {
    public abstract class TestsAssertions :TestsAids {
        private static BindingFlags allFlags => PublicFlagsFor.All | NonPublicFlagsFor.All;
        protected object objUnderTests;
        [TestCleanup] public virtual void TestCleanup() => objUnderTests = null;
        protected static void isAbstractMethod(object o, string method)
            => isTrue(o.GetType().BaseType.GetMethod(method, NonPublicFlagsFor.All).IsAbstract);
        protected static void areSame(object e, object a) => Assert.AreSame(e, a);
        protected static void areEqual<TExpected, TActual>(TExpected e, TActual a) => Assert.AreEqual(e, a);
        protected static void areEqual<TExpected, TActual>(TExpected e, TActual a, string s) => Assert.AreEqual(e, a, s);
        protected static void mostlyEqual<TExpected, TActual>(TExpected e, TActual a, string s = null) {
            var x = e.ToString();
            var y = e.ToString();
            for (var i = 0; i < x.Length; i++) {
                if (x[i] == y[i]) continue;
                if (i < x.Length / 2.0) areEqual(e, a, s);
            }
        }
        protected static void areNotEqual<TExpected, TActual>(TExpected e, TActual a) => Assert.AreNotEqual(e, a);
        protected static void throwsException<T>(Action a) where T : Exception => Assert.ThrowsException<T>(a);
        protected static void isGuid(string s) => isTrue(Guid.TryParse(s, out _));
        protected static void isNull(object o, string msg = null) => Assert.IsNull(o, msg ?? string.Empty);
        protected static void isNotNull(object o) => Assert.IsNotNull(o);
        protected static void isNotNull(object o, string message) => Assert.IsNotNull(o, message);
        protected static void isInstanceOfType<TType>(object o) => isInstanceOfType(o, typeof(TType));
        protected static void isInstanceOfType(object o, Type t) => Assert.IsInstanceOfType(o, t);
        protected static void isFalse(bool b, string s = null) {
            if (s is null) Assert.IsFalse(b);
            else Assert.IsFalse(b, s);
        }
        protected static void isTrue(bool b, string s = null) {
            if (s is null) Assert.IsTrue(b);
            else Assert.IsTrue(b, s);
        }
        protected static void fail(string message) => Assert.Fail(message);
        protected static void notTested(string message = null) => Assert.Inconclusive(message);
        protected static void notTested(string message, params object[] parameters)
            => Assert.Inconclusive(message, parameters);
        protected static void isReadOnly(object o, string propertyName, object expected) {
            var actual = isReadOnly(o, propertyName);
            areEqual(expected, actual);
        }
        protected static object isReadOnly(object o, string propertyName) {
            var p = o.GetType().GetProperty(propertyName, allFlags);
            isNotNull(p);
            isFalse(p?.CanWrite ?? true);
            isTrue(p?.CanRead ?? false);
            return p?.GetValue(o);
        }
        protected void isReadOnly(object expected, bool firstCharToLowerCaseInName = false) {
            var n = getNameAfter(nameof(isReadOnly));
            if (firstCharToLowerCaseInName) n = firstCharToLower(n);
            isReadOnly(objUnderTests, n, expected);
        }
        protected object isReadOnly() {
            var n = getNameAfter(nameof(isReadOnly));
            return isReadOnly(objUnderTests, n);
        }
        protected static void allPropertiesAreEqual(object x, object y, params string[] except) {
            string[] exceptHistory = { "Recorded", "Replaced", "RecordedBy", "RecordedWhy", "Timestamp" };
            foreach (var property in x.GetType().GetProperties(PublicFlagsFor.Instance)) {
                var name = property.Name;
                if (except.Contains(name)) continue;
                if (exceptHistory.Contains(name)) continue;
                var p = y.GetType().GetProperty(name, PublicFlagsFor.Instance);
                isNotNull(p, $"No property with name '{name}' found.");
                var expected = property.GetValue(x);
                var actual = p?.GetValue(y);
                areEqual(expected, actual, $"For property'{name}'.");
            }
        }
        protected static void notEqualProperties(object x, object y) {
            foreach (var property in x.GetType().GetProperties(PublicFlagsFor.Instance)) {
                var name = property.Name;
                var p = y.GetType().GetProperty(name, PublicFlagsFor.Instance);
                isNotNull(p, $"No property with name '{name}' found.");
                var expected = property.GetValue(x);
                var actual = p?.GetValue(y);
                if (expected != actual) return;
            }
            fail("All properties are same");
        }
        protected static void allPropertiesAreNotEqual(object x, object y, params string[] except) {
            foreach (var property in x.GetType().GetProperties(PublicFlagsFor.Instance)) {
                var name = property.Name;
                if (except.Contains(name)) continue;
                var p = y.GetType().GetProperty(name, PublicFlagsFor.Instance);
                isNotNull(p, $"No property with name '{name}' found.");
                var expected = property.GetValue(x);
                var actual = p?.GetValue(y);
                Assert.AreNotEqual(expected, actual, $"For property'{name}'.");
            }
        }
        protected static void htmlContains(IReadOnlyList<object> actual, IReadOnlyList<string> expected) {
            isInstanceOfType(actual, typeof(List<object>));
            areEqual(expected.Count, actual.Count);
            for (var i = 0; i < actual.Count; i++) {
                var a = actual[i].ToString();
                var e = expected[i];
                isTrue(a?.Contains(e) ?? false, $"{e} != {a}");
            }
        }
        protected static void isNullableProperty<T>(Func<T> get, Action<T> set) {
            isProperty(get, set);
            set(default);
            Assert.IsNull(get());
        }
        protected static void isProperty<T>(Func<T> get, Action<T> set) {
            var d = random<T>();
            while (true) {
                if (!d.Equals(get())) break;
                d = random<T>();
            }
            Assert.AreNotEqual(d, get());
            set(d);
            Assert.AreEqual(d, get());
        }
        protected static void isProperty(Func<bool> get, Action<bool> set) {
            var d = !get();
            Assert.AreNotEqual(d, get());
            set(d);
            Assert.AreEqual(d, get());
        }
        protected static void isNullableProperty(object o, string name, Type t) {
            isProperty(o, name, t);
            var p = o.GetType().GetProperty(name);
            canSetValue(o, p, null);
        }
        protected static void isProperty(object o, string name, Type t) {
            var p = isReadWriteProperty(o, name, t);
            canSetValue(o, p, random(t));
        }
        protected static void canSetValue(object o, PropertyInfo p, object v) {
            p.SetValue(o, v);
            Assert.AreEqual(v, p.GetValue(o));
        }
        protected static PropertyInfo isReadWriteProperty(object o, string name, Type t) {
            var p = o.GetType().GetProperty(name);
            Assert.IsNotNull(p);
            Assert.AreEqual(t, p.PropertyType);
            Assert.IsTrue(p.CanWrite);
            Assert.IsTrue(p.CanRead);
            return p;
        }
        protected void hasDisplayName(string propertyName, string displayName) {
            var actual = GetMember.DisplayName(propertyName, objUnderTests.GetType());
            areEqual(displayName, actual);
        }
        protected void isRequiredProperty(string propertyName, bool isUnique) {
            var actual = GetMember.IsRequired(propertyName, objUnderTests.GetType());
            areEqual(isUnique, actual);
        }
        protected void hasColumnName(string propertyName, string columnName) {
            var actual = GetMember.ColumnName(propertyName, objUnderTests.GetType());
            areEqual(columnName, actual);
        }
        protected void isProperty<TType>(string propertyName, string displayName) {
            isProperty(objUnderTests, propertyName, typeof(TType));
            hasDisplayName(propertyName, displayName);
        }
        protected void isNullableProperty<TType>(string propertyName, string displayName) {
            isNullableProperty(objUnderTests, propertyName, typeof(TType));
            hasDisplayName(propertyName, displayName);
        }
        protected void isProperty<TType>() {
            var n = getNameAfter(nameof(isProperty));
            isProperty(objUnderTests, n, typeof(TType));
        }
        protected void isNullable<TType>() {
            var n = getNameAfter(nameof(isNullable));
            isNullableProperty(objUnderTests, n, typeof(TType));
        }
        protected void isProperty<TType>(string displayName, bool isRequired = false) {
            var n = getNameAfter(nameof(isProperty));
            isProperty(objUnderTests, n, typeof(TType));
            hasDisplayName(n, displayName);
            isRequiredProperty(n, isRequired);
        }
        protected void isNullableProperty<TType>(string displayName, bool isRequired = false) {
            var n = getNameAfter(nameof(isNullableProperty));
            isNullableProperty(objUnderTests, n, typeof(TType));
            hasDisplayName(n, displayName);
            isRequiredProperty(n, isRequired);
        }
        protected void isNullableColumn<TType>(string columnName) {
            var n = getNameAfter(nameof(isNullableColumn));
            isNullableProperty(objUnderTests, n, typeof(TType));
            hasColumnName(n, columnName);
        }
    }
}
