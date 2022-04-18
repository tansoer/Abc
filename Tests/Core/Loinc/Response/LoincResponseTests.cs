using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Core.Loinc.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Abc.Tests.Core.Loinc.Response {
    [TestClass] public class LoincResponseTests: SealedTests<LoincResponse, object > {
        [TestMethod] public void IdTest() => isNullableColumn<string>("LOINC_NUM");
        [TestMethod] public void ComponentTest() => isNullableColumn<string>("COMPONENT");
        [TestMethod] public void ScaleTest() => isNullableColumn<string>("SCALE_TYP");
        [TestMethod] public void SystemTest() => isNullableColumn<string>("SYSTEM");
        [TestMethod] public void ClassTest() => isNullableColumn<string>("CLASS");
        [TestMethod] public void TimeAspectTest() => isNullableColumn<string>("TIME_ASPCT");
        [TestMethod] public void LongCommonNameTest() => isNullableColumn<string>("LONG_COMMON_NAME");
        [TestMethod] public void ExampleUnitsTest() => isNullableColumn<string>("EXAMPLE_UCUM_UNITS");
        [TestMethod] public void ReplaceTest() {
            var l = GetRandom.ObjectOf<List<string>>();
            var expected = l.ToSeparatedString(';');
            var c = GetRandom.Int32(0, l.Count-1);
            var s = l[c];
            var x = random<string>();
            LoincResponse.replace(l, s, x);
            areEqual(x, l[c]);
            var actual = l.ToSeparatedString(';');
            expected = expected.Replace(s, x);
            areEqual(expected, actual);
        }
        [TestMethod] public void GetUnitCodesTest() {
            var l = GetRandom.ObjectOf<List<string>>();
            obj.ExampleUnits = l.ToSeparatedString(';');
            var units = obj.GetUnitCodes();
            areEqual(l.Count, units.Count);
            foreach (var e in l) isTrue(units.Contains(e));
        }
    }
}
