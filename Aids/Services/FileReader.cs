using Abc.Aids.Extensions;
using Abc.Aids.Methods;
using System.Collections.Generic;
using System.Reflection;

namespace Abc.Aids.Services {
    public abstract class FileReader<T> where T: class, new() {
        protected internal string[] columnNames;
        protected internal T item;
        protected internal List<T> items;
        protected internal int rowIdx;
        protected internal object uniqueValue;
        protected internal List<object> uniqueValues;

        public bool NamesInFirstRow { get; }
        public string FileName { get; }
        public string UniqueProperty { get; }

        protected FileReader(string fileName, bool namesInFirstRow, string uniqueProperty = null) {
            FileName = fileName;
            NamesInFirstRow = namesInFirstRow;
            UniqueProperty = uniqueProperty;
        }

        public virtual List<T> Read(List<RowClassMapper> mappers) {
            initRead();
            openFile();
            while (!endOfFile()) {
                var row = readRow();
                rowIdx++;
                if (isNamesRow(row)) continue;
                readRows(row, mappers);
                if (isValueUnique()) addItem();
            }
            return items?? new List<T>();
        }
        internal virtual void addItem() {
            if (item is not null) items?.Add(item);
        }
        internal virtual void createNewItem(int colIdx, List<RowClassMapper> mappers) {
            if (colIdx != 0) return;
            item = new T();
            setCommonValues(mappers);
        }
        internal abstract bool endOfFile();
        internal virtual string getColumnName(int colIdx) => Safe.Run(() => columnNames[colIdx], string.Empty);
        internal virtual List<RowClassMapper> getMappers(List<RowClassMapper> mappers, int colIdx) {
            var l = new List<RowClassMapper>();
            return Safe.Run(() => {
                foreach (var e in mappers?? new List<RowClassMapper>() ) {
                    if (!isIndexOrNameType(e)) continue;
                    if (isIndexOrNameValue(e, colIdx)) l.Add(e);
                }
                return l;
            }, l);
        }
        internal PropertyInfo getPropertyInfo(string name) => item?.GetType()?.GetProperty(name);
        internal virtual object getPropertyValue(RowClassMapper m, string value)
            => Safe.Run(() => {
                if (m?.FormatStr == null) return value;
                return string.Format(m.FormatStr, value);
            }, value);
        internal virtual void initRead() {
            items = new List<T>();
            item = null;
            rowIdx = 0;
            uniqueValues = new List<object>();
        }
        internal virtual bool isIndexOrNameValue(RowClassMapper e, int colIdx) {
            var v = e?.Value?.ToString();
            var t = e?.ValueType ?? RowClassMapperType.Unspecified;
            var n = getColumnName(colIdx);
            return isThisIndex(v, t, colIdx) || isThisName(v, t, n);
        }
        internal virtual bool isThisIndex(string s, RowClassMapperType t, int colIdx) {
            if (t != RowClassMapperType.ColumnIndex) return false;
            if (s != colIdx.ToString() ) return false;
            return true;
        }
        internal virtual bool isThisName(string s, RowClassMapperType t, string name) {
            if (t != RowClassMapperType.ColumnName) return false;
            if (s != name) return false;
            return true;
        }
        internal static bool isIndexOrNameType(RowClassMapper e) => e?.ValueType 
            is RowClassMapperType.ColumnIndex or RowClassMapperType.ColumnName;
        internal virtual bool isNamesRow(string[] row) {
            var b = rowIdx == 1 && NamesInFirstRow;
            if (b) readNames(row);
            return b;
        }
        internal virtual bool isValueUnique() {
            if (uniqueValue is null) return true;
            if (uniqueValues.Contains(uniqueValue)) return false;
            uniqueValues.Add(uniqueValue);
            return true;
        }
        internal abstract void openFile();
        internal abstract void readNames(string[] row);
        internal virtual void readRows(string[] row, List<RowClassMapper> mappers) {
            var colCount = 0;
            foreach (var column in row) {
                createNewItem(colCount, mappers);
                foreach (var m in getMappers(mappers, colCount))
                    setProperty(m, column);
                colCount++;
            }
        }
        internal abstract string[] readRow();
        internal virtual void setCommonValue(RowClassMapper m) {
            if (m?.ValueType != RowClassMapperType.Value) return;
            var pi = getPropertyInfo(m.Name);
            setValue( pi, m.Value);
        }
        internal virtual void setCommonValues(List<RowClassMapper> l) {
            foreach (var e in l?? new List<RowClassMapper>()) setCommonValue(e);
        }
        internal virtual void setProperty(RowClassMapper m, string value) {
            var n = m?.Name;
            var pi = getProperty(n);
            var v = getPropertyValue(m, value);
            setUniqueValue(n, v);
            setValue(pi, v);
        }
        internal virtual PropertyInfo getProperty(string name) => Safe.Run(() => typeof(T).GetProperty(name), (PropertyInfo) null);
        internal virtual void setUniqueValue(string name, object v) {
            if (name == UniqueProperty) uniqueValue = v;
        }
        internal virtual void setValue(PropertyInfo p, object value) {
            var v = Variable.TryParse(value?.ToString()?? string.Empty, p?.PropertyType);
            if (p?.CanWrite ?? false) p?.SetValue(item, v);
        }
    }
}
