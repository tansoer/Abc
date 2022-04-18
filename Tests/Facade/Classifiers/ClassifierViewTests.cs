using Abc.Data.Classifiers;
using Abc.Facade.Classifiers;
using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Classifiers {
    [TestClass]
    public class ClassifierViewTests :SealedTests<ClassifierView, EntityTypeView> {
        [TestMethod] public void ClassifierTypeTest() => isProperty<ClassifierType>();
    }
}
