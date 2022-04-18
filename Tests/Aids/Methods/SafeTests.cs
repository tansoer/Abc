using System;
using System.Collections.Generic;
using System.Threading;
using Abc.Aids.Logging;
using Abc.Aids.Methods;
using Abc.Tests.Aids.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Aids.Methods {

    [TestClass] public class SafeTests :TestsBase {
        private LogTests.testLogBook logBook;
        [TestInitialize] public void TestInitialize() {
            type = typeof(Safe);
            logBook = new LogTests.testLogBook();
            Log.logBook = logBook;
        }
        [TestCleanup]
        public override void TestCleanup() {
            base.TestCleanup();
            Log.logBook = null;
        }
        [TestMethod] public void RunTest() { }
        [TestMethod] public void RunFunctionTest() {
            var actual = Safe.Run(() => "Result", "Error");
            Assert.AreEqual("Result", actual);
            Assert.IsNull(logBook.LoggedException);
        }
        [TestMethod] public void RunFailingFunctionTest() {
            var actual = Safe.Run(() => throw new ArgumentException(), "Error");
            Assert.AreEqual("Error", actual);
            var exception = logBook.LoggedException;
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
        }
        [TestMethod] public async Task RunAsyncTest() {
            var actual = await Safe.RunAsync(() => throw new ArgumentException(), "Error");
            Assert.AreEqual("Error", actual);
            var exception = logBook.LoggedException;
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
        }
        [TestMethod] public void RunMethodTest() {
            var newLogBook = new LogTests.testLogBook();
            Safe.Run(() => Log.logBook = newLogBook);
            Assert.IsNull(newLogBook.LoggedException);
        }
        [TestMethod] public void RunFailingMethodTest() {
            Safe.Run(() => throw new ArgumentOutOfRangeException());
            var exception = logBook.LoggedException;
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentOutOfRangeException));
        }
        [TestMethod] public void RunInsideRunTest() {
            Safe.Run(() => {
                Safe.Run(() => throw new ArgumentException());

                throw new AggregateException();
            });
            Assert.AreEqual(2, logBook.LoggedExceptions.Count);
            Assert.IsInstanceOfType(logBook.LoggedExceptions[0], typeof(ArgumentException));
            Assert.IsInstanceOfType(logBook.LoggedExceptions[1], typeof(AggregateException));
        }
        [TestMethod] public void RunInsideRunLockedTest() {
            Safe.Run(() => {
                Safe.Run(() => throw new ArgumentException(), true);

                throw new AggregateException();
            }, true);
            Assert.AreEqual(2, logBook.LoggedExceptions.Count);
            Assert.IsInstanceOfType(logBook.LoggedExceptions[0], typeof(ArgumentException));
            Assert.IsInstanceOfType(logBook.LoggedExceptions[1], typeof(AggregateException));
        }
        [TestMethod] public void RunInSeparateThreadsTest() {
            var list = new List<string>();
            startThreads(list);
            validateList(list);
            Assert.AreEqual(2, logBook.LoggedExceptions.Count);
            Assert.IsInstanceOfType(logBook.LoggedExceptions[0], typeof(ArgumentNullException));
            Assert.IsInstanceOfType(logBook.LoggedExceptions[1], typeof(ArithmeticException));
        }
        private static void startThreads(ICollection<string> l) {
            var m1 = "method1";
            var m2 = "method2";
            var t1 = new Thread(() => method(l, $"{m1}: ", () => throw new ArgumentNullException(m1)));
            var t2 = new Thread(() => method(l, $"{m2}: ", () => throw new ArithmeticException(m2)));
            t1.Start();
            Thread.Sleep(1000);
            t2.Start();
            Thread.Sleep(1000);
        }
        private static void method(ICollection<string> list, string message, Action exception) {
            Safe.Run(() => {
                Safe.Run(() => {
                    for (var i = 0; i < 10; i++) {
                        list.Add(message + DateTime.Now);
                        Thread.Sleep(1);
                    }

                    exception();
                }, true);
                list.Add(message + DateTime.Now);
            }, true);
        }
        private static void validateList(IReadOnlyList<string> l) {
            Assert.AreEqual(22, l.Count);

            for (var i = 0; i < 22; i++) {
                Assert.IsTrue(
                    i < 11
                        ? l[i].StartsWith("method1:")
                        : l[i].StartsWith("method2:"),
                    $"list[{i}] = {l[i]}");
            }
        }
    }
}




