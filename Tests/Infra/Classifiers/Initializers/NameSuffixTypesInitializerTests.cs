using System.Linq;
using Abc.Infra.Classifiers;
using Abc.Infra.Classifiers.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Classifiers.Initializers {
    [TestClass]
    public class NameSuffixTypesInitializerTests :DbInitializerTests<ClassifierDb> {

        public NameSuffixTypesInitializerTests() {
            type = typeof(NameSuffixTypesInitializer);
            db = new ClassifierDb(options);
            removeAll(db.Classifiers);
            NameSuffixTypesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() => areEqual(17, db.Classifiers.Count());
    }
}