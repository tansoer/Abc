using Abc.Data.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Classifiers {
    [TestClass] public class ClassifierTypeTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(ClassifierType);
        [TestMethod] public void UnspecifiedTest() => areEqual(0, (int)ClassifierType.Unspecified);
        [TestMethod] public void OrderTest() => areEqual(1, (int)ClassifierType.Order);
        [TestMethod] public void PartyCapabilityTest() => areEqual(2, (int)ClassifierType.PartyCapability);
        [TestMethod] public void ContactUsageTest() => areEqual(3, (int)ClassifierType.ContactUsage);
        [TestMethod] public void PersonNamePrefixTest() => areEqual(4, (int)ClassifierType.PersonNamePrefix);
        [TestMethod] public void RegistrationAuthorityTest() => areEqual(5, (int)ClassifierType.RegistrationAuthority);
        [TestMethod] public void PartyNameUsageTest() => areEqual(6, (int)ClassifierType.PartyNameUsage);
        [TestMethod] public void RegisteredIdentifierTest() => areEqual(7, (int)ClassifierType.RegisteredIdentifier);
        [TestMethod] public void PartyEthnicityTest() => areEqual(8, (int)ClassifierType.PartyEthnicity);
        [TestMethod] public void PersonNameSuffixTest() => areEqual(9, (int)ClassifierType.PersonNameSuffix);
        [TestMethod] public void OrganizationTest() => areEqual(10, (int)ClassifierType.Organization);
        [TestMethod] public void PartyPreferenceTest() => areEqual(11, (int)ClassifierType.PartyPreference);
        [TestMethod] public void PartyRoleInOrderTest() => areEqual(12, (int)ClassifierType.PartyRoleInOrder);
        [TestMethod] public void ProcessPriorityTest() => areEqual(13, (int)ClassifierType.ProcessPriority);
        [TestMethod] public void ActionStatusTest() => areEqual(14, (int)ClassifierType.ActionStatus);
        [TestMethod] public void SalesChannelTest() => areEqual(15, (int)ClassifierType.SalesChannel);
        [TestMethod] public void TermsAndConditionsTest() => areEqual(16, (int)ClassifierType.TermsAndConditions);
        [TestMethod] public void TaxationTypeTest() => areEqual(17, (int)ClassifierType.TaxationType);
    }
}