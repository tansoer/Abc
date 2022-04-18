using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;

namespace Abc.Pages.Classifiers {
    public sealed class PersonNameSuffixTypesPage
        :ClassifierPage<PersonNameSuffixTypesPage> {
        protected override ClassifierType classifierType => ClassifierType.PersonNameSuffix;
        protected internal override string pageUrl => ClassifierUrls.PersonNameSuffixTypes;
        protected override string title => ClassifierTitles.PersonNameSuffixTypes;
        public PersonNameSuffixTypesPage(IClassifiersRepo r) : base(r) { }
    }
}

