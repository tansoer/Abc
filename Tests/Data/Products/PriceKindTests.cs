using Abc.Aids.Reflection;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class PriceKindTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(PriceKind);

        [TestMethod]
        public void CountTest() => areEqual(5, GetEnum.Count<PriceKind>());

        [TestMethod]
        public void UnspecifiedTest() => areEqual(0, (int) PriceKind.Unspecified);

        [TestMethod]
        public void PossibleTest() => areEqual(1, (int) PriceKind.Possible);

        [TestMethod]
        public void AgreedTest() => areEqual(2, (int) PriceKind.Agreed);

        [TestMethod]
        public void ArbitraryTest() => areEqual(3, (int) PriceKind.Arbitrary);

        [TestMethod]
        public void DiscountTest() => areEqual(4, (int)PriceKind.Discount);

    }

}