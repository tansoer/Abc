using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests {
    public abstract class AssemblyBaseTests: TestsAssertions {
        [TestInitialize] public void CreateList() => list = new List<string>();
        [TestMethod] public void IsTested() => isAllTested(assembly);
        private static string isNotTested => "<{0}> is not tested";
        private static string noClassesInAssembly => "No classes in the assembly {0}";
        private static string noClassesInNamespace => "No classes in the namespace {0}";
        private static string testAssembly => "Abc.Tests";
        protected virtual string assembly => "Abc";
        private static char genericsClass => '`';
        private static char internalClass => '+';
        private static string displayClass => "<>";
        private static string shell32Class => "Shell32.";
        private List<string> list;
        protected virtual string nameSpace(string name) => $"{assembly}.{name}";
        protected virtual void isAllTested(string assembly, string namespaceName = null) {
            namespaceName ??= assembly;
            var l = getTypes(assembly);
            removeInterfaces(l);
            list = getNames(l);
            removeNotIn(namespaceName);
            removeSurrogates();
            removeTested();
            if (list.Count == 0) return;
            notTested(isNotTested, list[0]);
        }
        private static List<Type> getTypes(string assembly) {
            var l = GetSolution.TypesForAssembly(assembly);
            if (l.Count == 0) notTested(noClassesInAssembly, assembly);
            return l;
        }
        private static void removeInterfaces(IList<Type> types) {
            for (var i = types.Count; i > 0; i--) {
                var e = types[i - 1];
                if (!e.IsInterface) continue;
                types.Remove(e);
            }
        }
        private static List<string> getNames(List<Type> l) => l.Select(o => o.FullName).ToList();
        private void removeNotIn(string namespaceName) {
            if (string.IsNullOrEmpty(namespaceName)) return;
            list.RemoveAll(o => !o.StartsWith(namespaceName + '.'));
            if (list.Count > 0) return;
            notTested(noClassesInNamespace, namespaceName);
        }
        private void removeSurrogates() {
            list.RemoveAll(o => o.Contains(shell32Class));
            list.RemoveAll(o => o.Contains(internalClass));
            list.RemoveAll(o => o.Contains(displayClass));
            list.RemoveAll(o => o.Contains("<"));
            list.RemoveAll(o => o.Contains(">"));
            list.RemoveAll(o => o.Contains("Migrations"));
        }
        private void removeTested() {
            var tests = getTestClasses();
            for (var i = list.Count; i > 0; i--) {
                var className = list[i - 1];
                var testName = toTestName(className);
                var t = tests.Find(o => o.EndsWith(testName));
                if (t is null) continue;
                list.RemoveAt(i - 1);
            }
        }
        private static List<string> getTestClasses() {
            var l = new List<string>();
            var tests = GetSolution.TypeNamesForAssembly(testAssembly);
            foreach (var t in tests) {
                var n = removeGenericsChars(t);
                l.Add(n);
            }
            return l;
        }
        private string toTestName(string className) {
            className = removeAssemblyName(className);
            className = removeGenericsChars(className);
            return className + "Tests";
        }
        private static string removeGenericsChars(string className) {
            var idx = className.IndexOf(genericsClass);
            if (idx > 0) className = className.Substring(0, idx);
            return className;
        }
        private string removeAssemblyName(string className) => className[assembly.Length..];
    }
}
