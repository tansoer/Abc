using Abc.Aids.Random;
using Abc.Core.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Units {

    [TestClass]
    public class UnitInfoTests : TestsBase {

        private string id;
        private string code;
        private string name;
        private string def;
        private double factor;
        private TermInfo[] terms;
        private UnitInfo uFactor;
        private UnitInfo uTerms;

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(UnitInfo);
            id = rndStr;
            code = rndStr;
            name = rndStr;
            def = rndStr;
            factor = GetRandom.Double(-1000, 1000);
            terms = GetRandom.List(() => new TermInfo(rndStr, GetRandom.Int8())).ToArray();
            uFactor = new UnitInfo(id, code, name, def, factor);
            uTerms = new UnitInfo(id, code, name, def, terms);
        }

        [TestMethod] public void IdTest() => Assert.AreEqual(id, uFactor.Id);

        [TestMethod] public void CodeTest() => Assert.AreEqual(code, uFactor.Code);

        [TestMethod] public void NameTest() => Assert.AreEqual(name, uFactor.Name);

        [TestMethod] public void DefinitionTest() => Assert.AreEqual(def, uFactor.Definition);

        [TestMethod] public void FactorTest() => Assert.AreEqual(factor, uFactor.Factor);

        [TestMethod] public void TermsTest() => Assert.AreEqual(terms.Length, uTerms.Terms.Count);
        [TestMethod]
        public void NullTest() {
            uFactor = new UnitInfo(null, null, factor);
            Assert.AreEqual(null, uFactor.Id);
            Assert.AreEqual(null, uFactor.Code);
            Assert.AreEqual(null, uFactor.Name);
            Assert.AreEqual(null, uFactor.Definition);
            Assert.AreEqual(factor, uFactor.Factor);
            Assert.AreEqual(0, uFactor.Terms.Count);

        }

        [TestMethod]
        public void IdNotNullTest() {
            uFactor = new UnitInfo(id, null, factor);
            Assert.AreEqual(id, uFactor.Id);
            Assert.AreEqual(id, uFactor.Code);
            Assert.AreEqual(id, uFactor.Name);
            Assert.AreEqual(null, uFactor.Definition);
            Assert.AreEqual(factor, uFactor.Factor);
            Assert.AreEqual(0, uFactor.Terms.Count);

        }

    }

}