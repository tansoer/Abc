using Abc.Data.Classifiers;
using Abc.Domain.Classifiers.Parties;
using Abc.Facade.Classifiers;
using Abc.Pages.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Classifier.Pages.PersonNamePrefixTypes {
    public abstract class PersonNamePrefixTypesTests :BaseClassifierTests {
        protected override string baseUrl() => ClassifierUrls.PersonNamePrefixTypes;
        protected override ClassifierType? classifierType => ClassifierType.PersonNamePrefix;
        protected override ClassifierView toView(ClassifierData d) => new ClassifierViewFactory().Create(new NamePrefixType(d));
    }
    [TestClass] public class CreatePageTests :PersonNamePrefixTypesTests { }
    [TestClass] public class DeletePageTests :PersonNamePrefixTypesTests { }
    [TestClass] public class DetailsPageTests :PersonNamePrefixTypesTests { }
    [TestClass] public class EditPageTests :PersonNamePrefixTypesTests { }
    [TestClass] public class IndexPageTests :PersonNamePrefixTypesTests { }

}
