using Abc.Infra.Classifiers;
using Abc.Infra.Classifiers.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
namespace Abc.Tests.Infra.Classifiers.Initializers {
    [TestClass]
    public class NamePrefixTypesInitializerTests :DbInitializerTests<ClassifierDb> {

        public NamePrefixTypesInitializerTests() {
            type = typeof(NamePrefixTypesInitializer);
            db = new ClassifierDb(options);
            removeAll(db.Classifiers);
            NamePrefixTypesInitializer.Initialize(db);
        }

        [TestMethod] public void InitializeTest() => areEqual(10, db.Classifiers.Count());
    }
}
