using System;
using Abc.Tests.Data;
using Abc.Tests.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common {

    public abstract class AbstractPageTests<TClass, TBaseClass> : AbstractTests<TClass, TBaseClass> where TClass: class {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
        internal testRepo db;
        internal class testClass : TestPage {
            private readonly string page;
            public testClass(ITestRepo r, Uri u = null) : base(r) 
                => page = u?.ToString()?? "/Test";
            protected internal override string pageUrl => page;
        }
        internal class testRepo :mockRepo<TestObject, TestData>, ITestRepo { }

        [TestInitialize]
        public override void TestInitialize() {
            db = new testRepo();
            base.TestInitialize();
        }
    }
}
