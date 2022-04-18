using Abc.Data.Classifiers;
using Abc.Domain.Classifiers.Orders;
using Abc.Facade.Classifiers;
using Abc.Pages.Orders;
using Abc.Tests.Soft.Areas.Classifier;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Orders.Pages.TermsAndConditionsTypes {

    public abstract class TermsAndConditionsTypesTests :BaseClassifierTests {

        protected override string baseUrl() => OrderUrls.TermsAndConditionsTypes;
        protected override ClassifierType? classifierType => ClassifierType.TermsAndConditions;
        protected override ClassifierView toView(ClassifierData d)
            => new ClassifierViewFactory().Create(new TermsAndConditionsType(d));
    }
    [TestClass] public class CreatePageTests :TermsAndConditionsTypesTests { }
    [TestClass] public class DeletePageTests :TermsAndConditionsTypesTests { }
    [TestClass] public class DetailsPageTests :TermsAndConditionsTypesTests { }
    [TestClass] public class EditPageTests :TermsAndConditionsTypesTests { }
    [TestClass] public class IndexPageTests :TermsAndConditionsTypesTests { }

}
