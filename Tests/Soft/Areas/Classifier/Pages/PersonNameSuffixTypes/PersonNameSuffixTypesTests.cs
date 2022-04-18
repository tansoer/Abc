using Abc.Data.Classifiers;
using Abc.Domain.Classifiers.Parties;
using Abc.Facade.Classifiers;
using Abc.Pages.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Classifier.Pages.PersonNameSuffixTypes {

    public abstract class PersonNameSuffixTypesTests :BaseClassifierTests {

        protected override string baseUrl() => ClassifierUrls.PersonNameSuffixTypes;
        protected override ClassifierType? classifierType => ClassifierType.PersonNameSuffix;
        protected override ClassifierView toView(ClassifierData d) => new ClassifierViewFactory().Create(new NameSuffixType(d));
    }
    [TestClass] public class CreatePageTests :PersonNameSuffixTypesTests { }
    [TestClass] public class DeletePageTests :PersonNameSuffixTypesTests { }
    [TestClass] public class DetailsPageTests :PersonNameSuffixTypesTests { }
    [TestClass] public class EditPageTests :PersonNameSuffixTypesTests { }
    [TestClass] public class IndexPageTests :PersonNameSuffixTypesTests { }

}
