using Abc.Aids.Enums;
using Abc.Aids.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Enums {

    [TestClass] public class ContactTypeTests : TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(ContactType);
        [TestMethod] public void CountTest()
            => Assert.AreEqual(5, GetEnum.Count<ContactType>());
        [TestMethod] public void UnspecifiedTest() =>
            Assert.AreEqual(0, (int) ContactType.Unspecified);
        [TestMethod] public void EmailTest() =>
            Assert.AreEqual(1, (int) ContactType.Email);
        [TestMethod] public void WebTest() =>
            Assert.AreEqual(2, (int) ContactType.Web);
        [TestMethod] public void TelecomTest() =>
            Assert.AreEqual(3, (int) ContactType.Telecom);
        [TestMethod] public void PostalTest() =>
            Assert.AreEqual(4, (int) ContactType.Postal);
    }
    [TestClass]
    public class ContactTypeExtensionsTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(ContactTypeExtensions);
 
        [DataRow(ContactType.Web, false)]
        [DataRow(ContactType.Email, false)]
        [DataRow(ContactType.Telecom, false)]
        [DataRow(ContactType.Postal, true)]
        [DataRow(ContactType.Unspecified, false)]
        [DataTestMethod]
        public void IsPostalTest(ContactType t, bool expected)
            => Assert.AreEqual(expected, t.IsPostal());
        [DataRow(ContactType.Web, true)]
        [DataRow(ContactType.Email, false)]
        [DataRow(ContactType.Telecom, false)]
        [DataRow(ContactType.Postal, false)]
        [DataRow(ContactType.Unspecified, false)]
        [DataTestMethod]
        public void IsWebTest(ContactType t, bool expected) 
            => Assert.AreEqual(expected, t.IsWeb());
        [DataRow(ContactType.Web, false)]
        [DataRow(ContactType.Email, true)]
        [DataRow(ContactType.Telecom, false)]
        [DataRow(ContactType.Postal, false)]
        [DataRow(ContactType.Unspecified, false)]
        [DataTestMethod]
        public void IsEmailTest(ContactType t, bool expected)
            => Assert.AreEqual(expected, t.IsEmail());
        [DataRow(ContactType.Web, false)]
        [DataRow(ContactType.Email, false)]
        [DataRow(ContactType.Telecom, true)]
        [DataRow(ContactType.Postal, false)]
        [DataRow(ContactType.Unspecified, false)]
        [DataTestMethod]
        public void IsTelecomTest(ContactType t, bool expected)
            => Assert.AreEqual(expected, t.IsTelecom());
    }
}

