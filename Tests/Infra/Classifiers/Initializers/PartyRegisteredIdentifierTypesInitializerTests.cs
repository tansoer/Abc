using Abc.Infra.Classifiers;
using Abc.Infra.Classifiers.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Classifiers.Initializers
{
    [TestClass]
    public class PartyRegisteredIdentifierTypesInitializerTests :DbInitializerTests<ClassifierDb> {
        public PartyRegisteredIdentifierTypesInitializerTests() { 
            type = typeof(PartyRegisteredIdentifierTypesInitializer);
            db = new ClassifierDb(options);
            removeAll(db.Classifiers);
            PartyRegisteredIdentifierTypesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() => areEqual(6, db.Classifiers.Count());
    }
}