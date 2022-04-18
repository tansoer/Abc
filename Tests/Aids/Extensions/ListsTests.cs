using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Extensions {
    [TestClass] public class ListsTests : TestsBase {
        private List<int> list;
        private string expectedToSeparatedString;

        [TestInitialize] public void TestInitialize() {
            type = typeof(Lists);
            list = new List<int> { 1, 3, 2, 4, 1, 6 };
            expectedToSeparatedString = "1,3,2,4,1,6";
        }
        [TestMethod] public void OrderByTest() {
            list = Lists.OrderBy(list, x => x.ToString()).ToList();
            areEqual(6, list.Count);
            areEqual(1, list[0]);
            areEqual(1, list[1]);
            areEqual(2, list[2]);
            areEqual(3, list[3]);
            areEqual(4, list[4]);
            areEqual(6, list[5]);
        }
        [TestMethod] public void DistinctTest() {
            list = Lists.Distinct(list).ToList();
            areEqual(5, list.Count);
            areEqual(1, list[0]);
            areEqual(3, list[1]);
            areEqual(2, list[2]);
            areEqual(4, list[3]);
            areEqual(6, list[4]);
        }
        [TestMethod] public void ConvertTest() {
            var l = Lists.Convert(list, x => x.ToString()).ToList();
            areEqual(6, l.Count);
            areEqual("1", l[0]);
            areEqual("3", l[1]);
            areEqual("2", l[2]);
            areEqual("4", l[3]);
            areEqual("1", l[4]);
            areEqual("6", l[5]);
        }
        [TestMethod] public void OrderByWithNullsTest() {
            areEqual(0, Lists.OrderBy(list, null).Count());
            areEqual(0, Lists.OrderBy<int>(null, x => x.ToString()).Count());
            areEqual(0, Lists.OrderBy<int>(null, null).Count());
        }
        [TestMethod] public void DistinctWithNullsTest() 
            => areEqual(0, Lists.Distinct((IEnumerable<int>) null).Count());
        [TestMethod] public void ConvertWithNullsTest() {
            areEqual(0, Lists.Convert<int, string>(list, null).Count());
            areEqual(0, Lists.Convert<int, string>(null, x => x.ToString()).Count());
            areEqual(0, Lists.Convert<int, string>(null, null).Count());
        }
        [TestMethod] public void ToSeparatedStringTest() 
            =>areEqual(expectedToSeparatedString, list.ToSeparatedString(','));
    }
}





