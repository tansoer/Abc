using Abc.Aids.Classes;
using Abc.Aids.Reflection;
using Abc.Data.Common;
using Abc.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Aids.Classes {
    [TestClass] public class ArePropertiesTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(AreProperties);
        [TestMethod] public void EqualTest() {
            TestData testProperty1 = getTestProperty();
            TestData testProperty2 = getTestProperty();
            isTrue(AreProperties.Equal(testProperty1, testProperty2));
            testProperty2 = getDifferentTestProperty();
            isFalse(AreProperties.Equal(testProperty1, testProperty2));
            isTrue(AreProperties.Equal(testProperty1, testProperty2, "Id", "Name"));
        }
        private TestData getTestProperty() {
            return new TestData {
                Id = "dummy",
                Name = "dummy",
                Code = "dummy"
            };
        }
        private TestData getDifferentTestProperty() {
            return new TestData {
                Id = "foo",
                Name = "foo",
                Code = "dummy"
            };
        }
        [TestMethod] public void GuidsTest() {
            TestData d1 = new TestData();
            TestData d2 = new TestData();
            isTrue(AreProperties.Guids(d1, d2, GetMember.Name<EntityBaseData>(x => x.Id)));
            d1.Id = rndStr;
            isFalse(AreProperties.Guids(d1, d2, GetMember.Name<EntityBaseData>(x => x.Id)));
            d2.Id = rndStr;
            isFalse(AreProperties.Guids(d1, d2, GetMember.Name<EntityBaseData>(x => x.Id)));
            d1.Id = Guid.NewGuid().ToString();
            isFalse(AreProperties.Guids(d1, d2, GetMember.Name<EntityBaseData>(x => x.Id)));
        }
    }
}
