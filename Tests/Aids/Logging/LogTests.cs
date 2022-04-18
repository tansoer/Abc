using System;
using System.Collections.Generic;
using Abc.Aids.Logging;
using Abc.Aids.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Logging {

    [TestClass]
    public class LogTests : TestsBase {

        internal class testLogBook : ILogBook {

            public string LoggedMessage { get; private set; }
            public Exception LoggedException { get; private set; }
            public List<Exception> LoggedExceptions { get; } = new List<Exception>();

            public void WriteEntry(string message) {
                LoggedMessage = message;
            }

            public void WriteEntry(Exception e) {
                LoggedException = e;
                LoggedExceptions.Add(e);
            }

        }

        private testLogBook logBook;

        [TestInitialize]
        public virtual void TestInitialize() {
            type = typeof(Log);
            logBook = new testLogBook();
        }

        [TestCleanup]
        public override void TestCleanup() {
            base.TestCleanup();
            Log.logBook = null;
        }

        [TestMethod]
        public void MessageTest() {
            var message = rndStr;
            Log.Message(message);
            Assert.IsNull(logBook.LoggedMessage);
            Log.logBook = logBook;
            Log.Message(message);
            Assert.AreEqual(message, logBook.LoggedMessage);
        }

        [TestMethod]
        public void ExceptionTest() {
            var exception = new NotImplementedException();
            Log.Exception(exception);
            Assert.IsNull(logBook.LoggedException);
            Log.logBook = logBook;
            Log.Exception(exception);
            Assert.AreEqual(exception, logBook.LoggedException);
        }

    }

}


