using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Pages.Classifiers;

namespace Abc.Pages.Orders {
    public sealed class TaxationTypesPage :ClassifierPage<TaxationTypesPage> {
        protected override string title => OrderTitles.TaxationTypes;
        protected override ClassifierType classifierType => ClassifierType.TaxationType;
        public TaxationTypesPage(IClassifiersRepo r) : base(r) { }
        protected internal override string pageUrl => OrderUrls.TaxationTypes;
    }
}