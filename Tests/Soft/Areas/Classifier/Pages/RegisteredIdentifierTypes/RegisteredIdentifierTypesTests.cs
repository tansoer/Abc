using Abc.Data.Classifiers;
using Abc.Domain.Parties.Identifiers;
using Abc.Facade.Classifiers;
using Abc.Pages.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Classifier.Pages.RegisteredIdentifierTypes {

    public abstract class RegisteredIdentifierTypesTests :BaseClassifierTests {

        protected override string baseUrl() => ClassifierUrls.RegisteredIdentifierTypes;
        protected override ClassifierType? classifierType => ClassifierType.RegisteredIdentifier;
        protected override ClassifierView toView(ClassifierData d) => new ClassifierViewFactory().Create(new IdentifierType(d));
    }
    [TestClass] public class CreatePageTests :RegisteredIdentifierTypesTests { }
    [TestClass] public class DeletePageTests :RegisteredIdentifierTypesTests { }
    [TestClass] public class DetailsPageTests :RegisteredIdentifierTypesTests { }
    [TestClass] public class EditPageTests :RegisteredIdentifierTypesTests { }
    [TestClass] public class IndexPageTests :RegisteredIdentifierTypesTests { }

}
