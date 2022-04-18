using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Pages.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Classifiers {
    [TestClass]
    public class RegistrationAuthoritiesPageTests :ClassifierPageBaseTests<
        RegistrationAuthoritiesPage> {
        protected override string pageTitle => ClassifierTitles.RegistrationAuthorities;
        protected override string pageUrl => ClassifierUrls.RegistrationAuthorities;
        internal override ClassifierType classifierType => ClassifierType.RegistrationAuthority;
        internal override RegistrationAuthoritiesPage createObject(IClassifiersRepo r) => new(r);
    }
}
