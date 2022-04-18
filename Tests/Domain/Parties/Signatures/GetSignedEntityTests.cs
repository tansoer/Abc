using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Signatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Signatures {

    [TestClass]
    public class GetSignedEntityTests : SealedTests<GetSignedEntity, object> {

        [TestMethod]
        public void ByIdTest() {
            var d = GetRandom.ObjectOf<SignedEntityTypeData>();
            d.Name = typeof(IBodyMetricsRepo).FullName;
            var m = GetRandom.ObjectOf<BodyMetricData>();
            GetRepo.Instance<IBodyMetricsRepo>().Add(SignedEntityFactory.Create(m) as IBodyMetric);
            obj = new GetSignedEntity(new SignedEntityType(d));
            var o = obj.ById(m.Id) as IBodyMetric;
            Assert.IsNotNull(o);
            allPropertiesAreEqual(m, o.Data);
        }
        [TestMethod]
        public void GetAssemblyTest()
            => Assert.AreEqual(type.Assembly, obj.getAssembly());
    }
}