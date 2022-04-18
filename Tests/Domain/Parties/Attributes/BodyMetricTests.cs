using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Signatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Attributes {

    [TestClass]
    public class BodyMetricTests
        : AbstractTests<BodyMetric<string>, BasePartyAttribute<BodyMetricData>> {

        private class testClass : BodyMetric<string> {
            public testClass(BodyMetricData d = null) : base(d) { }
            public override string Value => Data?.Value ?? Unspecified.String;
        }
        protected override BodyMetric<string> createObject() =>
            new testClass(GetRandom.ObjectOf<BodyMetricData>());
        [TestMethod] public void signatureIdTest() => isReadOnly(obj.Data.SignatureId);
        [TestMethod] public void typeIdTest() => isReadOnly(obj.Data.TypeId);
        [TestMethod] public void ValueTest() => isReadOnly(obj.Data.Value);
        [TestMethod] public async Task TypeTest()
            => await testItemAsync<BodyMetricTypeData, BodyMetricType, IBodyMetricTypesRepo>(
                obj.typeId, () => obj.Type.Data, x => new BodyMetricType(x));
        [TestMethod] public async Task SignatureTest()
            => await testPartyAttributeAsync<PartySignatureData, PartySignature, IPartySignaturesRepo>(
                obj.signatureId, () => obj.Signature.Data, x => new PartySignature(x));
    }
}
