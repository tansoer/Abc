using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Contacts {

    [TestClass]
    public class EmailAddressTests : SealedTests<EmailAddress, PartyContact<PartyContactData>> {

        protected override EmailAddress createObject() => new EmailAddress(GetRandom.ObjectOf<PartyContactData>());

        [TestMethod] public void EmailTest() => isReadOnly(obj.Data.Name);

        [TestMethod] public void ToStringTest() => Assert.AreEqual(obj.Email, obj.ToString());
    }

}
