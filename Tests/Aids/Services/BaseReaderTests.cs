using System.Collections.Generic;
using Abc.Aids.Random;
using Abc.Aids.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Abc.Tests.Aids.Services {
    public abstract class BaseReaderTests<TClass, TBaseClass> :SealedTests<TClass, TBaseClass> where TClass : FileReader<TestItem> {
        private readonly List<RowClassMapper> withNames = new() {
            new RowClassMapper { Name = "StringProperty", Value = "Name", ValueType = RowClassMapperType.ColumnName },
            new RowClassMapper { Name = "DoubleProperty", Value = "Value", ValueType = RowClassMapperType.ColumnName },
            new RowClassMapper { Name = "IntegerProperty", Value = "Index", ValueType = RowClassMapperType.ColumnName },
            new RowClassMapper { Name = "ConstantProperty", Value = rndStr, ValueType = RowClassMapperType.Value },
        };
        private readonly List<RowClassMapper> withIndexes = new() {
            new RowClassMapper { Name = "StringProperty", Value = 0, ValueType = RowClassMapperType.ColumnIndex },
            new RowClassMapper { Name = "DoubleProperty", Value = 1, ValueType = RowClassMapperType.ColumnIndex },
            new RowClassMapper { Name = "IntegerProperty", Value = 2, ValueType = RowClassMapperType.ColumnIndex },
            new RowClassMapper { Name = "ConstantProperty", Value = rndStr, ValueType = RowClassMapperType.Value },
        };
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void ReadTest(bool firstRowIsNames) {
            if (firstRowIsNames) readWithNamesTest();
            else readWithIndexesTest();
        }
        [TestMethod] public void TestsDirTest() => isTrue(getTestsDirectoryName.EndsWith(@"\Tests"), getTestsDirectoryName);
        [TestMethod]
        public void ExecutableAssemblyTest() {
            var s = Assembly.GetExecutingAssembly().Location;
            var expected = composeTestString(s);
            isTrue(s.EndsWith(expected), s);
        }
        private static string composeTestString(string s) {
            var r = @"\Tests\bin\Debug\net5.0\Abc.Tests.dll";
            if (s.Contains(@"\net6.0\")) r = r.Replace("net5.0", "net6.0");
            if (s.Contains(@"\Release\")) r = r.Replace("Debug", "Release");
            if (s.Contains(@"\release\")) r = r.Replace("Debug", "release");
            return r;
        }
        [TestMethod]
        public void TestsAidsServicesDirTest() =>
            isTrue(servicesDir.EndsWith(@"\Tests\Aids\Services\"), servicesDir);
        private static string servicesDir => getTestsDirectoryName + @"\Aids\Services\";
        [TestMethod]
        public void FilesInServicesDirTest() {
            var files = Directory.GetFiles(servicesDir);
            var s = files.Aggregate(string.Empty, (current, f) => current + (" " + Path.GetFileName(f)));
            areEqual(11, files.Length, s);
        }
        private void readWithIndexesTest() {
            var n = servicesDir + fileWithoutNames();
            obj = createReader(n, false);
            var l = obj.Read(withIndexes);
            Assert.AreEqual(3, l.Count);
            var str = getConstant(withIndexes);
            foreach (var e in l) validate(e, str);
        }

        private static string getConstant(List<RowClassMapper> mapper) {
            foreach (var e in mapper) {
                if (e.ValueType == RowClassMapperType.Value) return e.Value.ToString();
            }
            return null;
        }
        private static void validate(TestItem e, string str) {
            switch (e.StringProperty) {
                case "A":
                    validate(e, "A", 123.45, 1, str);
                    break;
                case "B":
                    validate(e, "B", 234.56, 2, str);
                    break;
                case "C":
                    validate(e, "C", 345.67, 3, str);
                    break;
                default:
                    Assert.Fail(e.StringProperty);
                    break;
            }
        }
        private static void validate(TestItem e, string name, double value, int index, string constant) {
            Assert.AreEqual(name, e.StringProperty);
            Assert.AreEqual(value, e.DoubleProperty);
            Assert.AreEqual(index, e.IntegerProperty);
            Assert.AreEqual(constant, e.ConstantProperty);
        }
        private void readWithNamesTest() {
            var s = servicesDir;
            var n = s + fileWithNames();
            obj = createReader(n, true);
            var l = obj.Read(withNames);
            Assert.AreEqual(3, l.Count);
            var str = getConstant(withNames);
            foreach (var e in l) validate(e, str);
        }
        protected abstract string fileWithoutNames();
        protected abstract string fileWithNames();
        protected abstract TClass createReader(string n, bool v);
    }
}