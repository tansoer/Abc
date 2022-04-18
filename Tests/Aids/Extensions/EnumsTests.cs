using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Extensions {
    [TestClass]
    public class EnumsTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(Abc.Aids.Extensions.Enums);

        [DataRow(ContactType.Web, "Web Address")]
        [DataRow(ContactType.Email, "Email Address")]
        [DataRow(ContactType.Telecom, "Phone Number")]
        [DataRow(ContactType.Postal, "Postal Address")]
        [DataRow(ContactType.Unspecified, "Unspecified")]
        [DataTestMethod]
        public void ToStrTest(ContactType t, string expected)
            => Assert.AreEqual(expected, t.ToStr());
    }
}
