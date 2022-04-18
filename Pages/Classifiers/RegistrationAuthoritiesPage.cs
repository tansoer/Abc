using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;

namespace Abc.Pages.Classifiers {
    public sealed class RegistrationAuthoritiesPage :ClassifierPage<RegistrationAuthoritiesPage> {
        protected override ClassifierType classifierType => ClassifierType.RegistrationAuthority;
        protected override string title => ClassifierTitles.RegistrationAuthorities;
        protected internal override string pageUrl => ClassifierUrls.RegistrationAuthorities;
        public RegistrationAuthoritiesPage(IClassifiersRepo r) : base(r) { }
    }
}

