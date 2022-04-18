using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;
using Abc.Domain.Parties.Names;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Names {

    [TestClass]
    public class NameSuffixTests : SealedTests<NameSuffix, Entity<NameSuffixData>> {

        protected override NameSuffix createObject() => new (random<NameSuffixData>());

        [TestMethod] public void typeIdTest() => isReadOnly(obj.Data.SuffixTypeId);

        [TestMethod]
        public async Task TypeTest() {
            var d = random<ClassifierData>();
            d.ClassifierType = ClassifierType.PersonNameSuffix;
            d.Id = obj.typeId;
            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>(d, () => obj.Type.Data, d => ClassifierFactory.Create(d));
        }
    }

}