using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Pages.Orders;
using Abc.Tests.Pages.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Orders {
    [TestClass]
    public class TaxationTypesPageTests :ClassifierPageBaseTests<
        TaxationTypesPage> {
        protected override string pageTitle => OrderTitles.TaxationTypes;

        protected override string pageUrl => OrderUrls.TaxationTypes;

        internal override ClassifierType classifierType => ClassifierType.TaxationType;

        internal override TaxationTypesPage createObject(IClassifiersRepo r) => new(r);
    }
}
