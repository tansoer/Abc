using Abc.Pages.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Classifiers {
    [TestClass]
    public class ClassifierUrlsTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(ClassifierUrls);
        [TestMethod] public void PersonNamePrefixTypesTest() => areEqual("/Classifiers/PersonNamePrefixTypes", ClassifierUrls.PersonNamePrefixTypes);
        [TestMethod] public void PersonNameSuffixTypesTest() => areEqual("/Classifiers/PersonNameSuffixTypes", ClassifierUrls.PersonNameSuffixTypes);
        [TestMethod] public void RegisteredIdentifierTypesTest() => areEqual("/Classifiers/RegisteredIdentifierTypes", ClassifierUrls.RegisteredIdentifierTypes);
        [TestMethod] public void RegistrationAuthoritiesTest() => areEqual("/Classifiers/RegistrationAuthorities", ClassifierUrls.RegistrationAuthorities);
        [TestMethod] public void ClassifiersTest() => areEqual("/Classifiers/Classifiers", ClassifierUrls.Classifiers);
    }

}
