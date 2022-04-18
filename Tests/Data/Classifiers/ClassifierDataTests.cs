using Abc.Data.Classifiers;
using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Classifiers {
    [TestClass]
    public class ClassifierDataTests :SealedTests<ClassifierData, EntityTypeData> {
        [TestMethod] public void ClassifierTypeTest() => isProperty<ClassifierType>();
    }
}