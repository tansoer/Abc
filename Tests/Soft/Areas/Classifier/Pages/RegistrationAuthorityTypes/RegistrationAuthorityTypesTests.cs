using Abc.Data.Classifiers;
using Abc.Domain.Parties.Identifiers;
using Abc.Facade.Classifiers;
using Abc.Pages.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Classifier.Pages.RegistrationAuthorityTypes {

    public abstract class RegistrationAuthorityTypesTests :BaseClassifierTests {

        protected override string baseUrl() => ClassifierUrls.RegistrationAuthorities;
        protected override ClassifierType? classifierType => ClassifierType.RegistrationAuthority;
        protected override string performTitleCorrection(string title) => title[0..^3];
        protected override ClassifierView toView(ClassifierData d) => new ClassifierViewFactory().Create(new AuthorityType(d));
    }
    [TestClass] public class CreatePageTests :RegistrationAuthorityTypesTests { }
    [TestClass] public class DeletePageTests :RegistrationAuthorityTypesTests { }
    [TestClass] public class DetailsPageTests :RegistrationAuthorityTypesTests { }
    [TestClass] public class EditPageTests :RegistrationAuthorityTypesTests { }
    [TestClass] public class IndexPageTests :RegistrationAuthorityTypesTests { }

}
