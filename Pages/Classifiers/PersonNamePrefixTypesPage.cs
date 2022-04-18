using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;

namespace Abc.Pages.Classifiers {
    public sealed class PersonNamePrefixTypesPage
        :ClassifierPage<PersonNamePrefixTypesPage> {
        protected override ClassifierType classifierType => ClassifierType.PersonNamePrefix;
        protected internal override string pageUrl => ClassifierUrls.PersonNamePrefixTypes;
        protected override string title => ClassifierTitles.PersonNamePrefixTypes;
        public PersonNamePrefixTypesPage(IClassifiersRepo r) : base(r) { }
    }
}

