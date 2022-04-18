using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Pages.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Classifiers {
    [TestClass]
    public class RegisteredIdentifierTypesPageTests :ClassifierPageBaseTests<
        RegisteredIdentifierTypesPage> {
        protected override string pageTitle => ClassifierTitles.RegisteredIdentifierTypes;
        protected override string pageUrl => ClassifierUrls.RegisteredIdentifierTypes;
        internal override ClassifierType classifierType => ClassifierType.RegisteredIdentifier;
        internal override RegisteredIdentifierTypesPage createObject(IClassifiersRepo r) => new(r);
    }
}
