using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Attributes {

    [TestClass] public class PersonEthnicityTests
        :SealedTests<PersonEthnicity, BasePartyAttribute<PersonEthnicityData>> {
        protected override PersonEthnicity createObject()
            => new(GetRandom.ObjectOf<PersonEthnicityData>());

        [TestMethod] public void EthnicityIdTest() => isReadOnly(obj.Data.EthnicityId);

        [TestMethod] public async Task EthnicityTest() {
            var d = random<ClassifierData>();
            d.ClassifierType = ClassifierType.PartyEthnicity;
            d.Id = obj.EthnicityId;
            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>(
                d, () => obj.Ethnicity.Data, ClassifierFactory.Create);
        }
    }
}
