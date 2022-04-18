using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;
using System;

namespace Abc.Aids.Services {

    public sealed class ExcelReader<T> :FileReader<T> where T : class, new() {

        private WorkbookPart workbook;
        private IEnumerable<Row> data;
        private int rowCount;

        public ExcelReader(string fileName, bool columnNamesInFirstRow) : base(fileName, columnNamesInFirstRow) { }

        private string[] readRow(Row row) {
            var l = new List<string>();
            if (row != null)
                foreach (var e in row) {
                    if (e is not Cell cell) continue;
                    var value = readValue(cell);
                    l?.Add(value);
                }
            return l?.ToArray() ?? Array.Empty<string>();
        }
        private string readValue(Cell cell) {
            var value = string.Empty;
            if (cell.DataType == null) return cell.CellValue?.Text;
            if (cell.DataType != CellValues.SharedString) return value;
            if (!int.TryParse(cell.InnerText, out int id)) return value;
            value = readValue(id);
            return value;
        }
        private string readValue(int id) {
            var item = workbook?.SharedStringTablePart?.SharedStringTable
                .Elements<SharedStringItem>().ElementAt(id);
            if (item?.Text != null) return item.Text.Text;
            return item?.InnerText != null ? item.InnerText : string.Empty;
        }

        internal override void openFile() {
            var doc = SpreadsheetDocument.Open(FileName, false);
            workbook = doc?.WorkbookPart;
            var sheets = workbook?.Workbook?.GetFirstChild<Sheets>();
            var sheet = sheets?.GetFirstChild<Sheet>();
            var workSheet = ((WorksheetPart)workbook?.GetPartById(sheet?.Id ?? string.Empty))?.Worksheet;
            data = workSheet?.GetFirstChild<SheetData>()?.Elements<Row>();
            rowCount = data?.Count() ?? 0;
        }

        internal override bool endOfFile() => rowIdx >= rowCount;
        internal override string[] readRow() => readRow(data.ElementAt(rowIdx));
        internal override void readNames(string[] row) => columnNames = row;
    }
}
