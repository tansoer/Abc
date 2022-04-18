using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class BatchCheckedByDataTests : SealedTests<BatchCheckedByData, EntityBaseData> {
        [TestMethod] public void PartySignatureIdTest() => isNullable<string>();
        [TestMethod] public void BatchIdTest() => isNullable<string>();

    }

}