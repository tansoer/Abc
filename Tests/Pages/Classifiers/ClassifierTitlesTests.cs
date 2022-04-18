using Abc.Pages.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Classifiers {
    [TestClass]
    public class ClassifierTitlesTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(ClassifierTitles);
        [TestMethod] public void ClassifiersTest() => areEqual("Classifiers", ClassifierTitles.Classifiers);
        [TestMethod] public void PersonNamePrefixTypesTest() => areEqual("Person name prefix types", ClassifierTitles.PersonNamePrefixTypes);
        [TestMethod] public void PersonNameSuffixTypesTest() => areEqual("Person name suffix types", ClassifierTitles.PersonNameSuffixTypes);
        [TestMethod] public void RegisteredIdentifierTypesTest() => areEqual("Registered identifier types", ClassifierTitles.RegisteredIdentifierTypes);
        [TestMethod] public void RegistrationAuthoritiesTest() => areEqual("Registration authorities", ClassifierTitles.RegistrationAuthorities);

    }
}