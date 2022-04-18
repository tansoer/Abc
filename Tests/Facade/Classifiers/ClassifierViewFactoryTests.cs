using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Facade.Classifiers;
using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Classifiers {
    [TestClass]
    public class ClassifierViewFactoryTests :
        SealedTests<ClassifierViewFactory, AbstractViewFactory<ClassifierData, IClassifier, ClassifierView>> {
    }
}
