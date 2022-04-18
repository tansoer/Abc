using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Pages.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Classifiers {
    [TestClass]
    public class PersonNamePrefixTypesPageTests :ClassifierPageBaseTests<
        PersonNamePrefixTypesPage> {
        protected override string pageTitle => ClassifierTitles.PersonNamePrefixTypes;
        protected override string pageUrl => ClassifierUrls.PersonNamePrefixTypes;
        internal override ClassifierType classifierType => ClassifierType.PersonNamePrefix;
        internal override PersonNamePrefixTypesPage createObject(IClassifiersRepo r) => new(r);
    }
}
