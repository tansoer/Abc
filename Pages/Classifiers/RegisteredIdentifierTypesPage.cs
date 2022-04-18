using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;

namespace Abc.Pages.Classifiers {
    public sealed class RegisteredIdentifierTypesPage
        :ClassifierPage<RegisteredIdentifierTypesPage> {
        protected override ClassifierType classifierType => ClassifierType.RegisteredIdentifier;
        protected internal override string pageUrl => ClassifierUrls.RegisteredIdentifierTypes;
        protected override string title => ClassifierTitles.RegisteredIdentifierTypes;
        public RegisteredIdentifierTypesPage(IClassifiersRepo r) : base(r) { }
    }
}

