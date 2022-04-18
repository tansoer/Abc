using Abc.Aids.Random;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Abc.Tests {
    public abstract class TestsAids {
        protected static StackTrace getStack() => new();
        protected static string getNameAfter(string methodName) {
            var stack = getStack();
            return getNameAfter(stack, methodName);
        }
        protected static string getNameAfter(StackTrace s, string methodName) {
            int i = methodFrameIdx(s, methodName);
            return nextName(s, i);
        }
        private static string nextName(StackTrace s, int i) {
            for (i += 1; i < s.FrameCount - 1; i++) {
                var n = s.GetFrame(i)?.GetMethod()?.Name;
                if (n is "MoveNext" or "Start") continue;
                return n?.Replace("Test", string.Empty);
            }
            return string.Empty;
        }
        private static int methodFrameIdx(StackTrace s, string methodName) {
            int idx = -1;
            for (var i = 0; i < s.FrameCount - 1; i++) {
                var n = s.GetFrame(i)?.GetMethod()?.Name;
                if (n == methodName) idx = i;
                else if (idx > -1 && n != methodName) break;
            }
            return idx;
        }
        protected static string firstCharToLower(string s) {
            if (string.IsNullOrWhiteSpace(s)) return string.Empty;
            if (s.Length == 1) return s.ToLower();
            return s[0].ToString().ToLower() + s[1..];
        }
        protected static T random<T>() => (T)GetRandom.ValueOf<T>();
        protected static int random(int min, int max) => GetRandom.Int32(min, max);
        protected static object random(Type t) => GetRandom.ValueOf(t);
        protected static bool rndBool => random<bool>();
        protected static DateTime rndDt => random<DateTime>();
        protected static string rndStr => random<string>();
        protected static object rndInt => GetRandom.AnyInt();
        protected static object rndDbl => GetRandom.AnyDouble();
        protected static string getTestsDirectoryName {
            get {
                var n = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var length = n?.IndexOf("Tests", StringComparison.Ordinal) ?? 0;
                length += 5;
                n = n?.Substring(0, length) ?? string.Empty;
                return n;
            }
        }
    }
}