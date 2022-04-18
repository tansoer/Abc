using System;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Products.Packets {

    [TestClass]
    public class ProductInclusionRuleFactoryTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(ProductInclusionRuleFactory);

        [DataTestMethod]
        [DataRow(ProductInclusionKind.Conditional, typeof(ProductInclusionRuleCondition))]
        [DataRow(ProductInclusionKind.Detail, typeof(ProductInclusionRuleDetail))]
        [DataRow(ProductInclusionKind.Ordinal, typeof(ProductInclusionRule))]
        [DataRow(ProductInclusionKind.Unspecified, typeof(ProductInclusionError))]
        public void CreateTest(ProductInclusionKind kind, Type t) {
            var d = GetRandom.ObjectOf<ProductInclusionRuleData>();
            d.InclusionKind = kind;
            var o = ProductInclusionRuleFactory.Create(d);
            Assert.IsInstanceOfType(o, t);
            allPropertiesAreEqual(d, o.Data);

        }

    }

}