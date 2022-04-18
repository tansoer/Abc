using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Formats {

    [TestClass]
    public class FormatTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Abc.Aids.Formats.Format);

        //    [TestMethod] public void ListConstantTest() 
        //        => Assert.AreEqual("List", Abc.Aids.Formats.Format.list);

        //    [TestMethod] public void ElementConstantTest() 
        //        => Assert.AreEqual("unspecified", Abc.Aids.Formats.Format.unspecified);
        //    [TestMethod]
        //    public void NoneConstantTest()
        //        => Assert.AreEqual("none", Abc.Aids.Formats.Format.none);
        //    [TestMethod]
        //    public void NullValueConstantTest()
        //        => Assert.AreEqual("null", Abc.Aids.Formats.Format.nullValue);

        //    [TestMethod] public void CommaFormatTest() 
        //        => Assert.AreEqual("{0}, {1}", Abc.Aids.Formats.Format.commaFormat);

        //    [TestMethod] public void EtcFormatTest() 
        //        => Assert.AreEqual("{0} ...", Abc.Aids.Formats.Format.etcFormat);

        //    [TestMethod] public void ElementFormatTest() 
        //        => Assert.AreEqual("{0}<{1}>[{2}] = {{{3}}}", Abc.Aids.Formats.Format.elementFormat);

        //    [TestMethod] public void TripleFormatTest() 
        //        => Assert.AreEqual("{0}{1}{2}", Abc.Aids.Formats.Format.tripleFormat);

        //    [TestMethod] public void DupleFormatTest() 
        //        => Assert.AreEqual("{0}{1} {2}", Abc.Aids.Formats.Format.dupleFormat);

        //    [TestMethod] public void CommaBetweenTest() {
        //        var x = rndStr;
        //        var y = rndStr;
        //        var expected = Abc.Aids.Formats.Format.commaFormat.Format(x, y);
        //        Assert.AreEqual(expected, Abc.Aids.Formats.Format.CommaBetween(x, y));
        //        Assert.AreEqual(string.Empty, Abc.Aids.Formats.Format.CommaBetween(null, null));
        //        Assert.AreEqual(x, Abc.Aids.Formats.Format.CommaBetween(x, null));
        //        Assert.AreEqual(y, Abc.Aids.Formats.Format.CommaBetween(null, y));
        //    }

        //    [TestMethod] public void MoreTest() {
        //        var s = rndStr;
        //        Assert.AreEqual(s + " ...", Abc.Aids.Formats.Format.More(s));
        //        Assert.AreEqual("...", Abc.Aids.Formats.Format.More(null));
        //        Assert.AreEqual("...", Abc.Aids.Formats.Format.More(string.Empty));
        //        Assert.AreEqual("...", Abc.Aids.Formats.Format.More("     "));
        //    }

        //    [TestMethod] public void ElementTest() {
        //        Assert.AreEqual("C<A>[1] = {B}", Abc.Aids.Formats.Format.Element("A", 1, "B", "C"));
        //        Assert.AreEqual("List<A>[1] = {B}", Abc.Aids.Formats.Format.Element("A", 1, "B"));
        //        Assert.AreEqual("List<A>[1] = {null}", Abc.Aids.Formats.Format.Element("A", 1, null));
        //        Assert.AreEqual("List<unspecified>[1] = {null}", Abc.Aids.Formats.Format.Element(null, 1, null));
        //    }

        //    [TestMethod] public void ListTest() {
        //        Assert.AreEqual("[none]", Abc.Aids.Formats.Format.List(null));
        //        var l = new List<string>();
        //        Assert.AreEqual("[none]", Abc.Aids.Formats.Format.List(l, 10));
        //        l.AddRange(new List<string>{"A", "B", "C", "D"});
        //        Assert.AreEqual("[A, B, C, D]", Abc.Aids.Formats.Format.List(l, 10));
        //        l.AddRange(new List<string> {"E", "F", "G"});
        //        Assert.AreEqual("[A, B, C, D ...]", Abc.Aids.Formats.Format.List(l, 10));
        //    }

        //    [TestMethod] public void GetTest() {
        //        Assert.AreEqual(Abc.Aids.Formats.Format.dupleFormat, Abc.Aids.Formats.Format.Get());
        //        Assert.AreEqual(Abc.Aids.Formats.Format.tripleFormat, Abc.Aids.Formats.Format.Get('.'));
        //        Assert.AreEqual(Abc.Aids.Formats.Format.dupleFormat, Abc.Aids.Formats.Format.Get(GetRandom.Char(max:(char)('.'-1))));
        //        Assert.AreEqual(Abc.Aids.Formats.Format.dupleFormat, Abc.Aids.Formats.Format.Get(GetRandom.Char((char)('.' + 1))));
        //    }
    }
}