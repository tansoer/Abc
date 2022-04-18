using Abc.Aids.Random;
using Abc.Aids.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Abc.Tests.Aids.Services {
    [TestClass] public class FileReaderTests :AbstractTests<FileReader<TestItem>, object> {
        private string fileName;
        private bool namesInFirstRow;
        private string uniqueProperty;
        private testClass testObj;
        private List<string> stack;

        private class testClass :FileReader<TestItem> {
            private bool fileEnd = true;
            public int CallBase = 0;
            private bool callBase => CallBase-- > 0;
            public List<string> Stack { get; } = new List<string>();
            public testClass(string fileName, bool namesInFirstRow, string uniqueProperty = null)
                : base(fileName, namesInFirstRow, uniqueProperty) { }
            private T register<T>(string name, T value) {
                Stack.Add(name);
                return value;
            }
            private bool isEndOfFile() => fileEnd = !fileEnd;
            internal override void addItem() {
                if (callBase) base.addItem(); else Stack.Add(nameof(addItem));
            }
            internal override void createNewItem(int colIdx, List<RowClassMapper> mappers) {
                if (callBase) base.createNewItem(colIdx, mappers);
                else Stack.Add(nameof(createNewItem));
            }
            internal override bool endOfFile() => register(nameof(endOfFile), isEndOfFile());
            internal override string getColumnName(int colIdx) {
                if (callBase) return base.getColumnName(colIdx);
                else return register(nameof(getColumnName), string.Empty);
            }
            internal override List<RowClassMapper> getMappers(List<RowClassMapper> mapper, int colIdx) {
                if (callBase) return base.getMappers(mapper, colIdx);
                else return register(nameof(getMappers), mapper);
            }
            internal override void initRead() { if (callBase) base.initRead(); else Stack.Add(nameof(initRead)); }
            internal override bool isIndexOrNameValue(RowClassMapper m, int colIdx) {
                if (callBase) return base.isIndexOrNameValue(m, colIdx); else return register(nameof(isIndexOrNameValue), true);
            }
            internal override bool isThisIndex(string s, RowClassMapperType t, int colIdx) {
                if (callBase) return base.isThisIndex(s, t, colIdx); else return register(nameof(isThisIndex), false);
            }
            internal override bool isThisName(string s, RowClassMapperType t, string name) {
                if (callBase) return base.isThisName(s, t, name); else return register(nameof(isThisName), true);
            }
            internal override bool isNamesRow(string[] row) {
                if (callBase) return base.isNamesRow(row);
                else return register(nameof(isNamesRow), false);
            }
            internal override bool isValueUnique() {
                if (callBase) return base.isValueUnique();
                else return register(nameof(isValueUnique), true);
            }
            internal override void openFile() => Stack.Add(nameof(openFile));
            internal override void readNames(string[] row) => Stack.Add(nameof(readNames));
            internal override void readRows(string[] row, List<RowClassMapper> mappers) {
                if (callBase) base.readRows(row, mappers); else Stack.Add(nameof(readRows));
            }
            internal override string[] readRow() => register(nameof(readRow), Array.Empty<string>());
            internal override void setCommonValue(RowClassMapper e) {
                if (callBase) base.setCommonValue(e); else Stack.Add(nameof(setCommonValue));
            }
            internal override void setCommonValues(List<RowClassMapper> m) {
                if (callBase) base.setCommonValues(m); else Stack.Add(nameof(setCommonValues));
            }
            internal override void setProperty(RowClassMapper m, string value) {
                if (callBase) base.setProperty(m, value); else register(nameof(setProperty), true);
            }
            internal override PropertyInfo getProperty(string name) {
                if (callBase) return base.getProperty(name); else return register(nameof(getProperty), (PropertyInfo)null);
            }
            internal override object getPropertyValue(RowClassMapper m, string name) {
                if (callBase) return base.getPropertyValue(m, name); else return register(nameof(getPropertyValue), (object)null);
            }
            internal override void setUniqueValue(string name, object o) {
                if (callBase) base.setUniqueValue(name, o); else Stack.Add(nameof(setUniqueValue));
            }
            internal override void setValue(PropertyInfo pi, object o) {
                if (callBase) base.setValue(pi, o); else Stack.Add(nameof(setValue));
            }
        }
        protected override FileReader<TestItem> createObject() => new testClass(fileName, namesInFirstRow, uniqueProperty);
        [TestInitialize] public override void TestInitialize() {
            fileName = random<string>();
            namesInFirstRow = random<bool>();
            uniqueProperty = random<string>();
            base.TestInitialize();
            testObj = obj as testClass;
            stack = testObj.Stack;
        }
        [TestMethod] public void FileNameTest() => isReadOnly(fileName);
        [TestMethod] public void NamesInFirstRowTest() => isReadOnly(namesInFirstRow);
        [TestMethod] public void UniquePropertyTest() => isReadOnly(uniqueProperty);
        [TestMethod] public void ReadTest() {
            var r = obj.Read(new List<RowClassMapper>());
            var s = (obj as testClass).Stack;
            areEqual(0, r.Count);
            areEqual(nameof(obj.initRead), s[0]);
            areEqual(nameof(obj.openFile), s[1]);
            areEqual(nameof(obj.endOfFile), s[2]);
            areEqual(nameof(obj.readRow), s[3]);
            areEqual(nameof(obj.isNamesRow), s[4]);
            areEqual(nameof(obj.readRows), s[5]);
            areEqual(nameof(obj.isValueUnique), s[6]);
            areEqual(nameof(obj.addItem), s[7]);
            areEqual(nameof(obj.endOfFile), s[8]);
            areEqual(9, s.Count);
        }
        [TestMethod] public void ReadRowsTest() {
            var row = new[] { string.Empty };
            var mappers = new List<RowClassMapper>() { new RowClassMapper() };
            testObj.CallBase = 1;

            obj.readRows(row, mappers);
            
            areEqual(nameof(obj.createNewItem), stack[0]);
            areEqual(nameof(obj.getMappers), stack[1]);
            areEqual(nameof(obj.setProperty), stack[2]);
            areEqual(3, stack.Count);
        }
 
        [DataRow(false, false)]
        [DataRow(false, true)]
        [DataRow(true, false)]
        [DataRow(true, true)]
        [TestMethod] public void AddItemTest(bool itemNotNull, bool itemsNotNull) {
            testObj.CallBase = 1;
            obj.items = itemsNotNull? new List<TestItem>(): null;
            obj.item = itemNotNull ? new TestItem() : null; 

            obj.addItem();

            if (itemNotNull && itemsNotNull) areEqual(obj.item, obj.items[0]);
            else if (itemsNotNull) areEqual(0, obj.items.Count);
            else isNull(obj.items);
        }
        
        [DataRow(true)]
        [DataRow(false)]
        [TestMethod] public void CreateNewItemTest(bool colIdxIsZero) {
            var colIdx = colIdxIsZero ? 0 : random<bool>() ? GetRandom.Int32(2) : GetRandom.Int32(max: 0);
            testObj.CallBase = 1;
            obj.item = null;

            obj.createNewItem(colIdx, null);

            if (colIdxIsZero) isNotNull(obj.item); else isNull(obj.item);
            if (colIdxIsZero) areEqual(nameof(obj.setCommonValues), stack[0]); else areEqual(0, stack.Count);
        }
        [TestMethod] public void EndOfFileTest() => isAbstractMethod(obj, nameof(obj.endOfFile));
        [TestMethod] public void GetColumnNameTest() {
            var colCount = GetRandom.Int32(3, 5);
            var colName = random<string>();
            var colIdx = GetRandom.Int32(0, colCount-1);
            var l = new List<string>();
            for (var i = 0; i < colCount; i++) {
                var s = i == colIdx? colName: random<string>();
                l.Add(s);
            }
            obj.columnNames = l.ToArray();
            testObj.CallBase = 1;

            areEqual(colName, obj.getColumnName(colIdx));
        }
        [TestMethod]
        public void GetMappersTest() {
            testObj.CallBase = 1000;
            var colIdx = random<int>();
            areEqual(0, obj.getMappers(null, colIdx).Count);
            areEqual(0, obj.getMappers(new List<RowClassMapper>(), colIdx).Count);
            areEqual(0, obj.getMappers(new List<RowClassMapper> { new RowClassMapper(), new RowClassMapper()}, 50).Count);
            var count = GetRandom.UInt8(10, 30);
            var startIdx = GetRandom.UInt8(0, count);
            var endIdx = GetRandom.UInt8(startIdx, count);
            var l = new List<RowClassMapper>();
            colIdx = GetRandom.UInt8(1, 5);
            obj.columnNames = new []{ "0", "1", "2", "3", "4", "5" };
            for (var i = 0; i < count; i++) {
                var m = new RowClassMapper();
                if (i >= startIdx && i <= endIdx) {
                    m.ValueType = random<bool>() ? RowClassMapperType.ColumnIndex : RowClassMapperType.ColumnName;
                    m.Value = m.ValueType is RowClassMapperType.ColumnIndex ? colIdx : colIdx.ToString();
                }
                l.Add(m);
            }
            var r = obj.getMappers(l, colIdx);
            areEqual(endIdx + 1 - startIdx, r.Count);
        }
        [DataRow(nameof(TestItem.DoubleProperty), true)]
        [DataRow(nameof(TestItem.ConstantProperty), true)]
        [DataRow(nameof(TestItem.IntegerProperty), true)]
        [DataRow(nameof(TestItem.StringProperty), true)]
        [DataRow(null, false)]
        [TestMethod] public void GetPropertyInfoTest(string s, bool isProperty) {
            s ??= random<string>();
            isNull(obj.getPropertyInfo(s));
            obj.item = new TestItem();
            if (isProperty) areEqual(s, obj.getPropertyInfo(s).Name);
            else isNull(obj.getPropertyInfo(s));
        }

        [DataRow("aaa {0}", true)]
        [DataRow(null, false)]
        [TestMethod]
        public void GetPropertyValueTest(string fmt, bool isFormated) {
            testObj.CallBase = 2;
            var v = random<string>();
            areEqual(v, obj.getPropertyValue(null, v));
            var m = new RowClassMapper { FormatStr = fmt };

            var actual = obj.getPropertyValue(m, v);

            var expected = isFormated? string.Format(fmt, v) : v;
            areEqual(expected, actual);
        }
        [TestMethod] public void InitReadTest() {
            testObj.CallBase = 1; 
            obj.items = null;
            obj.item = new TestItem();
            obj.rowIdx = random<int>();
            obj.uniqueValues = null;
            
            obj.initRead();
            
            areEqual(0, obj.items.Count);
            isNull(obj.item);
            areEqual(0, obj.rowIdx);
            areEqual(0, obj.uniqueValues.Count);
        }
        [TestMethod] public void IsIndexOrNameValueTest() {
            testObj.CallBase = 1;
            obj.isIndexOrNameValue(null, random<int>());
            areEqual(3, testObj.Stack.Count);
            areEqual(nameof(obj.getColumnName), testObj.Stack[0]);
            areEqual(nameof(obj.isThisIndex), testObj.Stack[1]);
            areEqual(nameof(obj.isThisName), testObj.Stack[2]);
        }

        [DataRow(RowClassMapperType.ColumnIndex, true)]
        [DataRow(RowClassMapperType.ColumnName, false)]
        [DataRow(RowClassMapperType.Value, false)]
        [DataRow(RowClassMapperType.Unspecified, false)]
        [TestMethod] public void IsThisIndex(RowClassMapperType t, bool isThis) {
            testObj.CallBase = 2;
            string v = random<string>();
            int colIdx = random<int>();
            areEqual(false, obj.isThisIndex(v, t, colIdx));
            areEqual(isThis, obj.isThisIndex(colIdx.ToString(), t, colIdx));
        }
        [DataRow(RowClassMapperType.ColumnIndex, false)]
        [DataRow(RowClassMapperType.ColumnName, true)]
        [DataRow(RowClassMapperType.Value, false)]
        [DataRow(RowClassMapperType.Unspecified, false)]
        [TestMethod] public void IsThisName(RowClassMapperType t, bool isThis) {
            testObj.CallBase = 2;
            string v = random<string>();
            string colName = random<string>();
            areEqual(false, obj.isThisName(v, t, colName));
            areEqual(isThis, obj.isThisName(colName, t, colName));
        }
        [DataRow(RowClassMapperType.ColumnIndex)]
        [DataRow(RowClassMapperType.ColumnName)]
        [DataRow(RowClassMapperType.Value)]
        [DataRow(RowClassMapperType.Unspecified)]
        [TestMethod]
        public void IsIndexOrNameTypeTest(RowClassMapperType t) {
            var m = new RowClassMapper {
                ValueType = t
            };
            var actual = FileReader<TestItem>.isIndexOrNameType(m);
            var expected = t is RowClassMapperType.ColumnName or RowClassMapperType.ColumnIndex;
            areEqual(expected, actual);
        }
        [DataRow(1, false, false)]
        [DataRow(1, true, true)]
        [DataRow(null, false, false)]
        [DataRow(null, true, false)]
        [TestMethod] public void IsNamesRowTest(int? rowId, bool namesInFirstRow, bool expected) {
            var id = rowId?? (random<bool>() ? GetRandom.Int32(max: 0) : GetRandom.Int32(2));
            obj = new testClass(null, namesInFirstRow);
            obj.rowIdx = id;
            testObj = obj as testClass;
            testObj.CallBase = 1;

            var actual = obj.isNamesRow(null);

            if (expected) areEqual( nameof(obj.readNames), testObj.Stack[0]);
            else areEqual(0, testObj.Stack.Count);
            areEqual(expected, actual);
        }
        [DataRow(true, false, true)]
        [DataRow(true, true, true)]
        [DataRow(false, false, true)]
        [DataRow(false, true, false)]
        [TestMethod] public void IsValueUniqueTest(bool isNull, bool isInList, bool excpected) {
            testObj.CallBase = 1;
            var o = isNull ? null : GetRandom.AnyValue();
            obj.uniqueValues = new List<object>();
            obj.uniqueValue = o;
            if (isInList) obj.uniqueValues.Add(o);
            var actual = obj.isValueUnique();
            areEqual(excpected, actual);
            if (!isNull) isTrue(obj.uniqueValues.Contains(o));
        }
        [TestMethod] public void OpenFileTest() => isAbstractMethod(obj, nameof(obj.openFile));
        [TestMethod] public void ReadNamesTest() => isAbstractMethod(obj, nameof(obj.readNames));
        [TestMethod] public void ReadRowTest() => isAbstractMethod(obj, nameof(obj.readRow));

        [DataRow(RowClassMapperType.Value, true)]
        [DataRow(RowClassMapperType.ColumnIndex, false)]
        [DataRow(RowClassMapperType.ColumnName, false)]
        [DataRow(RowClassMapperType.Unspecified, false)]
        [DataRow(null, false)]
        [TestMethod] public void SetCommonValueTest(RowClassMapperType? t, bool expected ) {
            void test(object v, object def, string name, Func<object> propertyValue) {
                obj.item = new TestItem();
                var m = new RowClassMapper {
                    ValueType = t ?? RowClassMapperType.Unspecified,
                    Value = v,
                    Name = name
                };
                testObj.CallBase = 10;
                obj.setCommonValue(t is null ? null : m);
                areEqual(expected ? v : def, propertyValue());
            }
            test(random<string>(), null, nameof(obj.item.StringProperty), () => obj.item.StringProperty);
            test(random<double>(), 0.0, nameof(obj.item.DoubleProperty), () => obj.item.DoubleProperty);
            test(random<int>(), 0, nameof(obj.item.IntegerProperty), () => obj.item.IntegerProperty);
        }

        [DataRow(null)]
        [DataRow(0)]
        [DataRow(1)]
        [TestMethod] public void SetCommonValuesTest(int? count) {
            testObj.CallBase = 1;
            var c  = ((count != 1)? count: GetRandom.UInt8(5, 10)) ?? 0;
            var mappers = new List<RowClassMapper>();
            for (var i = 0; i < c; i++) mappers.Add(random<RowClassMapper>());
            obj.setCommonValues(count is null ? null: mappers) ;
            areEqual(count != 1 ? 0: c, testObj.Stack.Count);
            for (var i =0; i < c; i++) areEqual(nameof(obj.setCommonValue), testObj.Stack[i]);
        }
        [TestMethod] public void SetPropertyTest() {
            testObj.CallBase = 1;
            obj.setProperty(null, null);
            areEqual(4, testObj.Stack.Count);
            areEqual(nameof(obj.getProperty), testObj.Stack[0]);
            areEqual(nameof(obj.getPropertyValue), testObj.Stack[1]);
            areEqual(nameof(obj.setUniqueValue), testObj.Stack[2]);
            areEqual(nameof(obj.setValue), testObj.Stack[3]);
        }
        [DataRow(nameof(TestItem.DoubleProperty), true)]
        [DataRow(nameof(TestItem.ConstantProperty), true)]
        [DataRow(nameof(TestItem.IntegerProperty), true)]
        [DataRow(nameof(TestItem.StringProperty), true)]
        [DataRow(null, false)]
        [TestMethod] public void GetPropertyTest(string name, bool expected) {
            testObj.CallBase = 1;
            obj.item = new TestItem();
            var pi = obj.getProperty(name);
            if (!expected) isNull(pi);
            else areEqual(name, pi.Name);
        }

        [DataRow(true)]
        [DataRow(false)]
        [TestMethod] public void SetUniqueValueTest(bool expected) {
            var name = random<string>();
            var val = GetRandom.AnyValue();
            obj = new testClass(null, random<bool>(), expected ? name : random<string>());
            testObj = obj as testClass;
            testObj.CallBase = 1;
            obj.setUniqueValue(name, val);
            if (!expected) isNull(obj.uniqueValue);
            else areEqual(val, obj.uniqueValue);
        }

        [DataRow(nameof(TestItem.DoubleProperty), typeof(double))]
        [DataRow(nameof(TestItem.ConstantProperty), typeof(string))]
        [DataRow(nameof(TestItem.IntegerProperty), typeof(int))]
        [DataRow(nameof(TestItem.StringProperty), typeof(string))]
        [DataRow(nameof(TestItem.DoubleProperty), null)]
        [DataRow(nameof(TestItem.ConstantProperty), null)]
        [DataRow(nameof(TestItem.IntegerProperty), null)]
        [DataRow(nameof(TestItem.StringProperty), null)]
        [DataRow(null, typeof(double))]
        [DataRow(null, typeof(string))]
        [DataRow(null, typeof(int))]
        [DataRow(null, typeof(string))]
        [DataRow(null, null)]
        [TestMethod]
        public void SetValueTest(string propertyName, Type valueType) {
            testObj.CallBase = 1;
            obj.item = new TestItem();
            var pi = (propertyName is null)? null: obj.item.GetType().GetProperty(propertyName);
            var o = (valueType is null)? null: GetRandom.ValueOf(valueType);
            obj.setValue(pi, o);
            if (propertyName == nameof(obj.item.ConstantProperty)) areEqual((valueType is null)? string.Empty: o, obj.item.ConstantProperty);
            else if (propertyName == nameof(obj.item.DoubleProperty)) areEqual((valueType is null) ? 0.0 : o, obj.item.DoubleProperty);
            else if (propertyName == nameof(obj.item.IntegerProperty)) areEqual((valueType is null) ? 0 : o, obj.item.IntegerProperty);
            else if (propertyName == nameof(obj.item.StringProperty)) areEqual((valueType is null) ? string.Empty : o, obj.item.StringProperty);
            else {
                areEqual((string)null, obj.item.ConstantProperty);
                areEqual(0.0, obj.item.DoubleProperty);
                areEqual(0, obj.item.IntegerProperty);
                areEqual((string)null, obj.item.StringProperty);
            }
        }
    }
}
