using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Identifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Identifiers {

    [TestClass] public class RegistrationAuthorityTests
        :SealedTests<RegistrationAuthority, BasePartyAttribute<RegistrationAuthorityData>> {

        protected override RegistrationAuthority createObject()
            => new (GetRandom.ObjectOf<RegistrationAuthorityData>());

        [TestMethod] public void typeIdTest() => isReadOnly(obj.Data.AuthorityTypeId);

        [TestMethod] public async Task TypeTest() {
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.Id = obj.typeId;
            d.ClassifierType = ClassifierType.RegistrationAuthority;

            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>(
                d, () => obj.Type.Data, x => new AuthorityType(x));
        }

        [TestMethod] public async Task PartyTest() {
            var d = GetRandom.ObjectOf<PartyData>();
            d.Id = obj.PartyId;
            d.PartyType = PartyType.Organization;
            await testItemAsync<PartyData, IParty, IPartiesRepo>(
                d, () => obj.Party.Data, x => new Organization(x));
        }
    }
}