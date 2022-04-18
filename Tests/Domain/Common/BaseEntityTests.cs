using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Domain.Common {
    [TestClass]  public class BaseEntityTests
        :AbstractTests<BaseEntity<MeasureData>, Value<MeasureData>> {
        private class testClass :BaseEntity<MeasureData> {
            public testClass(MeasureData d): base(d) { }
        }
        protected override BaseEntity<MeasureData> createObject() => new testClass(random<MeasureData>());
        [TestMethod] public void IdTest() => isReadOnly(obj.Id);
        [TestMethod] public void ValidFromTest() => isReadOnly(obj.Data.ValidFrom);
        [TestMethod] public void ValidToTest() => isReadOnly(obj.Data.ValidTo);
        [TestMethod] public void ToStringTest()
            => areEqual($"Id = {obj.Id} ({obj.GetType().FullName})", obj.ToString());
        [DataTestMethod] [DataRow(true)] [DataRow(false)] [DataRow(null)] public void IsRemovedTest(bool? isRemoved) {
            isReadOnly(!Archetype.isNull(obj.Data.ValidTo));
            var d = random<MeasureData>();
            d.ValidTo =
                (isRemoved is null) ? null
                : isRemoved == true ? rndDt
                : DateTime.MaxValue;
            obj = new testClass(d);
            areEqual(isRemoved ?? false, obj.IsRemoved);
        }
    }
}
