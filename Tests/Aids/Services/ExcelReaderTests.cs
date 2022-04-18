using Abc.Aids.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Services {
    [TestClass] public class ExcelReaderTests :BaseReaderTests<ExcelReader<TestItem>, FileReader<TestItem>> {

        protected override string fileWithoutNames() => @"FileWithoutNamesInFirstRow.xlsx";
        protected override string fileWithNames() => @"FileWithNamesInFirstRow.xlsx";
        protected override ExcelReader<TestItem> createReader(string fileName, bool namesInFirstRow) 
            => new (fileName, namesInFirstRow);
    }
}