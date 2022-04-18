using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Roles;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Signatures {

    [TestClass]
    public class BasePartySignatureTests :AbstractTests<BasePartySignature<PartySignatureData>,
        BasePartyAttribute<PartySignatureData>> {

        private class testClass :BasePartySignature<PartySignatureData> {

            public testClass(PartySignatureData d = null) : base(d) { }

        }
        protected override BasePartySignature<PartySignatureData> createObject()
            => new testClass(GetRandom.ObjectOf<PartySignatureData>());

        [TestMethod] public void IsSignedTest() => testIsSigned();

        private static void testIsSigned() {
            isFalse(new testClass().IsSigned());
            var d = GetRandom.ObjectOf<PartySignatureData>();
            d.ValidTo = null; 
            isTrue(new testClass(d).IsSigned());
            d.ValidFrom = Unspecified.ValidFromDate;
            isFalse(new testClass(d).IsSigned());

            d = GetRandom.ObjectOf<PartySignatureData>();
            isFalse(new testClass(d).IsSigned());
            d.ValidTo = Unspecified.ValidToDate;
            isTrue(new testClass(d).IsSigned());

            d = GetRandom.ObjectOf<PartySignatureData>();
            d.ValidTo = Unspecified.ValidToDate;
            isTrue(new testClass(d).IsSigned());
            d.PartyId = Unspecified.String;
            isFalse(new testClass(d).IsSigned());
        }
        [TestMethod] public void PartySummaryIdTest() => isReadOnly(obj.Data.PartySummaryId);
        [TestMethod] public void SignedEntityTypeIdTest() => isReadOnly(obj.Data.SignedEntityTypeId);
        [TestMethod] public void PartyAuthenticationIdTest() => isReadOnly(obj.Data.PartyAuthenticationId);
        [TestMethod] public void SignedEntityIdTest() => isReadOnly(obj.Data.SignedEntityId);
        [TestMethod] public async Task AuthenticationTest() =>
            await testItemAsync<AuthenticationData, Authentication, IAuthenticationsRepo>
            (obj.PartyAuthenticationId, () => obj.Authentication.Data, d => new Authentication(d));

        private PartySummaryData summaryData {
            get {
                var d = GetRandom.ObjectOf<PartySummaryData>();
                d.Id = obj.PartySummaryId;
                d.RoleInOrder = PartyRoleInOrder.Unspecified;
                return d;
            }
        }
        [TestMethod] public async Task PartySummaryTest() =>
            await testItemAsync<PartySummaryData, IPartySummary, IPartySummariesRepo>(
                summaryData, () => obj.PartySummary.Data, d => new PartySummary(d));

        [TestMethod] public void SignedEntityTest() {
            var d = GetRandom.ObjectOf<SignedEntityTypeData>();
            d.Name = typeof(IBodyMetricsRepo).FullName;
            d.Id = obj.SignedEntityTypeId;
            GetRepo.Instance<ISignedEntityTypesRepo>().Add(new SignedEntityType(d));
            var m = GetRandom.ObjectOf<BodyMetricData>();
            m.Id = obj.SignedEntityId;
            GetRepo.Instance<IBodyMetricsRepo>().Add(SignedEntityFactory.Create(m) as IBodyMetric);
            var o = isReadOnly() as IBodyMetric;
            Assert.IsNotNull(o);
            allPropertiesAreEqual(m, o.Data);
        }
        [TestMethod] public async Task SignedEntityTypeTest() =>
            await testItemAsync<SignedEntityTypeData, SignedEntityType, ISignedEntityTypesRepo>(
                obj.SignedEntityTypeId, () => obj.SignedEntityType.Data, d => new SignedEntityType(d));
    }
}