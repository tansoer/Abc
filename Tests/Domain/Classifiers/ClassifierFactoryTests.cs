using Abc.Aids.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Identifiers;
using Abc.Domain.Parties.Names;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Preferences;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers.Orders;
using Abc.Domain.Classifiers.Parties;
using Abc.Domain.Classifiers;

namespace Abc.Tests.Domain.Classifiers {

    [TestClass]
    public class ClassifierFactoryTests :TestsBase {

        private ClassifierData d;

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(ClassifierFactory);
            d = GetRandom.ObjectOf<ClassifierData>();
        }

        [TestMethod] public void CreateTest() { }

        [DataRow(ClassifierType.Order, typeof(OrderType))]
        [DataRow(ClassifierType.PartyCapability, typeof(CapabilityType))]
        [DataRow(ClassifierType.ContactUsage, typeof(ContactUsageType))]
        [DataRow(ClassifierType.PersonNamePrefix, typeof(NamePrefixType))]
        [DataRow(ClassifierType.RegistrationAuthority, typeof(AuthorityType))]
        [DataRow(ClassifierType.PartyNameUsage, typeof(NameUsageType))]
        [DataRow(ClassifierType.RegisteredIdentifier, typeof(IdentifierType))]
        [DataRow(ClassifierType.PartyEthnicity, typeof(Ethnicity))]
        [DataRow(ClassifierType.PersonNameSuffix, typeof(NameSuffixType))]
        [DataRow(ClassifierType.Organization, typeof(OrganizationType))]
        [DataRow(ClassifierType.PartyPreference, typeof(PreferenceType))]
        [TestMethod]
        public void CreateSpecificTypeTest(ClassifierType type, Type domainObjType) {
            d.ClassifierType = type;
            var o = ClassifierFactory.Create(d);
            Assert.IsInstanceOfType(o, domainObjType);
            allPropertiesAreEqual(d, o.Data);
        }

        [DataRow(ClassifierType.Order)]
        [DataRow(ClassifierType.PartyCapability)]
        [DataRow(ClassifierType.ContactUsage)]
        [DataRow(ClassifierType.PersonNamePrefix)]
        [DataRow(ClassifierType.RegistrationAuthority)]
        [DataRow(ClassifierType.PartyNameUsage)]
        [DataRow(ClassifierType.RegisteredIdentifier)]
        [DataRow(ClassifierType.PartyEthnicity)]
        [DataRow(ClassifierType.PersonNameSuffix)]
        [DataRow(ClassifierType.Organization)]
        [DataRow(ClassifierType.PartyPreference)]
        [TestMethod]
        public void CreateSpecificTypeDataTest(ClassifierType type) {
            d.ClassifierType = type;
            var o = ClassifierFactory.Create(d);
            allPropertiesAreEqual(d, ClassifierFactory.Create(o));
        }
    }
}
