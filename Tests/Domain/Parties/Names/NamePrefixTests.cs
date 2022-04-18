using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;
using Abc.Domain.Parties.Names;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Names {

    [TestClass]
    public class NamePrefixTests : SealedTests<NamePrefix, Entity<NamePrefixData>> {

        protected override NamePrefix createObject() => new (random<NamePrefixData>());

        [TestMethod] public void PrefixTypeIdTest() => isReadOnly(obj.Data.PrefixTypeId);

        [TestMethod] public async Task TypeTest() {
            var d = random<ClassifierData>();
            d.ClassifierType = ClassifierType.PersonNamePrefix;
            d.Id = obj.PrefixTypeId;
            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>(d, () => obj.Type.Data, ClassifierFactory.Create);
        }
    }
}
