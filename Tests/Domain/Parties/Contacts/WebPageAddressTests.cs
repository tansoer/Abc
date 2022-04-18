using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Contacts {

    [TestClass]
    public class WebPageAddressTests : SealedTests<WebPageAddress, PartyContact<PartyContactData>> {

        protected override WebPageAddress createObject()
            => new WebPageAddress(GetRandom.ObjectOf<PartyContactData>());

        [TestMethod] public void UrlTest() => isReadOnly(obj.Data.Name);

        [TestMethod] public void ToStringTest() => Assert.AreEqual(obj.Url, obj.ToString());
    }

}
