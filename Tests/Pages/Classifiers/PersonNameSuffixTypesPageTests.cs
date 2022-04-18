using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Pages.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Classifiers {
    [TestClass]
    public class PersonNameSuffixTypesPageTests :ClassifierPageBaseTests<
        PersonNameSuffixTypesPage> {
        protected override string pageTitle => ClassifierTitles.PersonNameSuffixTypes;
        protected override string pageUrl => ClassifierUrls.PersonNameSuffixTypes;
        internal override ClassifierType classifierType => ClassifierType.PersonNameSuffix;
        internal override PersonNameSuffixTypesPage createObject(IClassifiersRepo r) => new(r);
    }
}
