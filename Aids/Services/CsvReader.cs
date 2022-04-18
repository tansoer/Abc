using Microsoft.VisualBasic.FileIO;

namespace Abc.Aids.Services {
    public sealed class CsvReader<T>: FileReader<T> where T: class, new() {
        public CsvReader(string fileName, bool columnNamesInFirstRow, string uniqueProperty = null)
            : base(fileName, columnNamesInFirstRow, uniqueProperty) { }
        private TextFieldParser file;
        internal override void openFile() {
            file = new TextFieldParser(FileName);
            file.SetDelimiters(",");
        }
        internal override bool endOfFile() => file?.EndOfData ?? true;
        internal override string[] readRow() => file.ReadFields();
        internal override void readNames(string[] row) => columnNames = row;
    }
}
