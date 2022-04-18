using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Classifiers {
    [TestClass] public class ClassifierTests :TestsHost {
        private ClassifierError obj;

        [TestInitialize] public void TestInitialize() {
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.ClassifierType = ClassifierType.Unspecified;
            obj = new ClassifierError(d);
            type = typeof(Classifier<ClassifierError>);
        }

        [TestMethod] public void ClassifierTypeTest() =>
            isReadOnly(obj, nameof(obj.ClassifierType), obj.Data.ClassifierType);

        [TestMethod] public void BaseTypeIdTest() => isReadOnly(obj, nameof(obj.BaseTypeId), obj.Data.BaseTypeId);

        [TestMethod] public async Task BaseTypeTest() {
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.ClassifierType = ClassifierType.Unspecified;
            d.Id = obj.BaseTypeId;
            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>
                (d, () => obj.BaseType.Data, ClassifierFactory.Create);
        }

        [DataTestMethod]
        [DataRow(ClassifierType.Order)]
        [DataRow(ClassifierType.PartyCapability)]
        [DataRow(ClassifierType.ContactUsage)]
        [DataRow(ClassifierType.PersonNamePrefix)]
        [DataRow(ClassifierType.RegistrationAuthority)]
        [DataRow(ClassifierType.PartyNameUsage)]
        [DataRow(ClassifierType.RegisteredIdentifier)]
        [DataRow(ClassifierType.PartyEthnicity)]
        [DataRow(ClassifierType.PersonNameSuffix)]
        [DataRow(ClassifierType.Organization)]
        [DataRow(ClassifierType.PartyPreference)]
        public void IsTypeOfTest(ClassifierType c) {
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.ClassifierType = c;
            var o = ClassifierFactory.Create(d);
            Assert.IsTrue(o.IsTypeOf(c));
        }

        [DataTestMethod]
        [DataRow(ClassifierType.Order)]
        [DataRow(ClassifierType.PartyCapability)]
        [DataRow(ClassifierType.ContactUsage)]
        [DataRow(ClassifierType.PersonNamePrefix)]
        [DataRow(ClassifierType.RegistrationAuthority)]
        [DataRow(ClassifierType.PartyNameUsage)]
        [DataRow(ClassifierType.RegisteredIdentifier)]
        [DataRow(ClassifierType.PartyEthnicity)]
        [DataRow(ClassifierType.PersonNameSuffix)]
        [DataRow(ClassifierType.Organization)]
        [DataRow(ClassifierType.PartyPreference)]
        public void IsNotTypeOfTest(ClassifierType c) {
            ClassifierType t;
            do { t = GetRandom.EnumOf<ClassifierType>(); } while (t == c);

            var d = GetRandom.ObjectOf<ClassifierData>();
            d.ClassifierType = t;
            var o = ClassifierFactory.Create(d);
            Assert.IsFalse(o.IsTypeOf(c));
        }
    }
}