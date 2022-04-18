using System.Collections.Generic;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Pages.Classifiers;
using Abc.Pages.Common.Aids;

namespace Abc.Pages.Orders {
    public sealed class TermsAndConditionsTypesPage :
        ClassifierPage<TermsAndConditionsTypesPage> {
        protected override string title => OrderTitles.TermsAndConditionsTypes;
        protected override ClassifierType classifierType => ClassifierType.TermsAndConditions;
        public TermsAndConditionsTypesPage(IClassifiersRepo r) : base(r) { }
        protected internal override string pageUrl => OrderUrls.TermsAndConditionsTypes;
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Timestamp)
        };
    }
}