using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;
using Abc.Domain.Parties.Names;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Names {

    [TestClass]
    public class PartyNameUseTests : SealedTests<PartyNameUse, Entity<PartyNameUseData>> {

        protected override PartyNameUse createObject() => new (random<PartyNameUseData>());
        [TestMethod] public void typeIdTest() => isReadOnly(obj.Data.NameUseTypeId);
        [TestMethod] public async Task TypeTest() {
            var d = random<ClassifierData>();
            d.ClassifierType = ClassifierType.PartyNameUsage;
            d.Id = obj.typeId;
            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>(d, () => obj.Type.Data, 
                d => ClassifierFactory.Create(d));
        }
    }
}
