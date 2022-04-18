using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class PriceEndorsementDataTests : SealedTests<PriceEndorsementData, EntityBaseData> {

        [TestMethod] public void PriceIdTest() => isNullable<string>();
        [TestMethod] public void PartySignatureIdTest() => isNullable<string>();

    }

}