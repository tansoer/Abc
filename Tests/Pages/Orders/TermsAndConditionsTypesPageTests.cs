using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Pages.Orders;
using Abc.Tests.Pages.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Orders {
    [TestClass]
    public class TermsAndConditionsTypesPageTests :ClassifierPageBaseTests<
        TermsAndConditionsTypesPage> {
        protected override string pageTitle => OrderTitles.TermsAndConditionsTypes;

        protected override string pageUrl => OrderUrls.TermsAndConditionsTypes;

        internal override ClassifierType classifierType => ClassifierType.TermsAndConditions;

        internal override TermsAndConditionsTypesPage createObject(IClassifiersRepo r) => new(r);
    }
}
