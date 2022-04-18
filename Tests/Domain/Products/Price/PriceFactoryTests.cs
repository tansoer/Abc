using System;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products.Price;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Products.Price {

    [TestClass]
    public class PriceFactoryTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(PriceFactory);

        [DataTestMethod]
        [DataRow(PriceKind.Possible, typeof(PossiblePrice))]
        [DataRow(PriceKind.Agreed, typeof(AgreedPrice))]
        [DataRow(PriceKind.Arbitrary, typeof(ArbitraryPrice))]
        [DataRow(PriceKind.Unspecified, typeof(PriceError))]
        public void CreateTest(PriceKind kind, Type t) {
            var d = GetRandom.ObjectOf<PriceData>();
            d.Kind = kind;
            var o = PriceFactory.Create(d);
            Assert.IsInstanceOfType(o, t);
            allPropertiesAreEqual(d, o.Data);

        }

    }

}