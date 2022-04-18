using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties {
    [TestClass] public class OrganizationTypeTests :SealedTests<OrganizationType, Classifier<OrganizationType>> {

        protected override OrganizationType createObject() {
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.ClassifierType = ClassifierType.Organization;
            return ClassifierFactory.Create(d) as OrganizationType;
        }

        [TestMethod] public void BaseTypeIdTest() => isReadOnly(obj.Data.BaseTypeId);
        [TestMethod] public void BaseTypeTest() {
            isReadOnly();
            var o = obj.BaseType;
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(OrganizationType));
            Assert.IsFalse(o.IsUnspecified);
        }

        [TestMethod] public async Task HasBaseTypeTest() {
            var d = random<ClassifierData>();
            d.Id = obj.BaseTypeId;
            d.ClassifierType = ClassifierType.Organization;
            d.BaseTypeId = null;
            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>(
                d, () => obj.BaseType.Data, ClassifierFactory.Create);
        }
    }
}
