using Abc.Aids.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Services {

    [TestClass]
    public class CsvReaderTests :BaseReaderTests<CsvReader<TestItem>, FileReader<TestItem>> {

        protected override string fileWithoutNames() => @"FileWithoutNamesInFirstRow.csv";
        protected override string fileWithNames() => @"FileWithNamesInFirstRow.csv";
        protected override CsvReader<TestItem> createReader(string fileName, bool namesInFirstRow)
                => new(fileName, namesInFirstRow);
    }
}
