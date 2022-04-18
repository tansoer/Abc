using System;
using Abc.Aids.Constants;
using Abc.Aids.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Constants {

    [TestClass]
    public class CharacterTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Character);

        [TestMethod] public void SpaceTest() => Assert.AreEqual(' ', Character.Space);

        [TestMethod] public void EolTest() => Assert.AreEqual('\n', Character.Eol);

        [TestMethod] public void ZeroTest() => Assert.AreEqual('0', Character.Zero);

        [TestMethod] public void UnderscoreTest() => Assert.AreEqual('_', Character.Underscore);

        [TestMethod] public void TabTest() => Assert.AreEqual('\t', Character.Tab);

        [TestMethod] public void CommaTest() => Assert.AreEqual(',', Character.Comma);
        [TestMethod] public void DotTest() => Assert.AreEqual('.', Character.Dot);

        [TestMethod]
        public void IsLetterTest() {
            Assert.IsTrue(Character.IsLetter(GetRandom.Char('a', 'z')));
            Assert.IsTrue(Character.IsLetter(GetRandom.Char('A', 'Z')));
            Assert.IsFalse(Character.IsLetter(GetRandom.Char('0', '9')));
            Assert.IsFalse(Character.IsLetter(GetRandom.Char(max: '/')));
        }

        [TestMethod]
        public void IsNumberTest() {
            Assert.IsFalse(Character.IsNumber(GetRandom.Char('a', 'z')));
            Assert.IsFalse(Character.IsNumber(GetRandom.Char('A', 'Z')));
            Assert.IsTrue(Character.IsNumber(GetRandom.Char('0', '9')));
            Assert.IsFalse(Character.IsNumber(GetRandom.Char(max: '/')));
        }

        [TestMethod] public void IsDotTest() => testIsCharacter(Character.IsDot, '.');

        private static void testIsCharacter(Func<char, bool> f, char c) {
            Assert.IsFalse(f(GetRandom.Char(max: (char) (c - 1))));
            Assert.IsFalse(f(GetRandom.Char((char) (c + 1))));
            Assert.IsTrue(f(c));
        }

        [TestMethod]
        public void IsCommaTest()
            => testIsCharacter(Character.IsComma, ',');

        [TestMethod] public void IsSpaceTest() => testIsCharacter(Character.IsSpace, ' ');

    }

}
