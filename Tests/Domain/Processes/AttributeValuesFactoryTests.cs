using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Attribute = Abc.Domain.Processes.Attribute;

namespace Abc.Tests.Domain.Processes {

    [TestClass]
    public class AttributeValuesFactoryTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(AttributeValuesFactory);

        [DataTestMethod]
        [DataRow(AttributeValueType.AttributeValue, typeof(Attribute))]
        [DataRow(AttributeValueType.PossibleValue, typeof(AttributePossibleValue))]
        public void CreateTest(AttributeValueType type, Type t) {
            var d = GetRandom.ObjectOf<AttributeValueData>();
            d.Type = type;
            var o = AttributeValuesFactory.Create(d);
            Assert.IsInstanceOfType(o, t);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}