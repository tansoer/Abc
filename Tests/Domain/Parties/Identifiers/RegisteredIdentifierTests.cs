using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Identifiers;
using Abc.Infra.Classifiers;
using Abc.Infra.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Identifiers {

    [TestClass] public class RegisteredIdentifierTests 
        : SealedTests<RegisteredIdentifier, BasePartyAttribute<RegisteredIdentifierData>> {

        private ClassifiersRepo classifiers;
        private PartyDb db;
        private RegistrationAuthoritiesRepo authorities;

        protected override RegisteredIdentifier createObject()
            => new (GetRandom.ObjectOf<RegisteredIdentifierData>());

        [TestCleanup]
        public override void TestCleanup() {
            removeAll(classifiers?.dbSet, db);
            removeAll(authorities?.dbSet, db);
            classifiers = null;
            authorities = null;
            db = null;
            base.TestCleanup();
        }

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            classifiers = getRepo<IClassifiersRepo, ClassifiersRepo>();
            authorities = getRepo<IRegistrationAuthoritiesRepo, RegistrationAuthoritiesRepo>();
            db = classifiers?.db as PartyDb;
        }
        [TestMethod] public void typeIdTest() => isReadOnly(obj.Data.RegisteredIdentifierTypeId);
        [TestMethod] public void authorityIdTest() => isReadOnly(obj.Data.RegistrationAuthorityId);
        [TestMethod] public async Task TypeTest() {
            var d = random<ClassifierData>();
            d.ClassifierType = ClassifierType.RegisteredIdentifier;
            d.Id = obj.typeId;
            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>(d, () => obj.Type.Data, ClassifierFactory.Create );
        }
        [TestMethod] public async Task AuthorityTest() =>
            await testItemAsync<RegistrationAuthorityData, RegistrationAuthority, IRegistrationAuthoritiesRepo>(
                obj.authorityId, () => obj.Authority.Data, d => new RegistrationAuthority(d));
        [TestMethod] public void IdentifierTest() => isReadOnly(obj.Data.Name);
        [TestMethod] public void NameTest() {
            var d = random<ClassifierData>();
            d.ClassifierType = ClassifierType.RegisteredIdentifier;
            d.Id = obj.typeId;
            classifiers.Add(ClassifierFactory.Create(d));
            isReadOnly(obj.Type?.Data.Name);
        }
    }
}
