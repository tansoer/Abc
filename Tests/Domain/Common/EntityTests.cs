using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Abc.Tests.Domain.Common {

    [TestClass] public class EntityTests
        :AbstractTests<Entity<MeasureData>, DetailedEntity<MeasureData>> {
        internal class MockEntity :IEntity {
            public Mock Mock { get; }
            public string Id => Mock.Func<string>();
            public string Name => Mock.Func<string>();
            public string Code => Mock.Func<string>();
            public string Details => Mock.Func<string>();
            public DateTime ValidFrom => Mock.Func<DateTime>();
            public DateTime ValidTo => Mock.Func<DateTime>();
            public bool IsUnspecified => Mock.Func<bool>();
            public MockEntity() { Mock = new Mock(); }
            public object GetParam(string key, int idx) => Mock.Parameters[key][idx];
            internal void testCallingStack(Action a, params string[] methodNames) {
                a();
                var stack = Mock.Stack;
                var prevIdx = 0;
                for (var i = methodNames.Length; i > 0; i--) {
                    var methodName = methodNames[i - 1];
                    var idx = methodFrameIdx(stack, methodName);
                    if (idx < 0) Assert.Fail($"Method <{methodName}> is not in stack");
                    if (idx < prevIdx)
                        Assert.Fail($"Method <{methodName}> is not calling {methodNames[prevIdx - 1]}");
                }
            }
            private static int methodFrameIdx(StackTrace s, string methodName) {
                int idx = -1;
                for (var i = 0; i < s.FrameCount - 1; i++) {
                    var n = s.GetFrame(i)?.GetMethod()?.Name;
                    if (n == methodName) idx = i;
                    else if (idx > -1 && n != methodName) break;
                }
                return idx;
            }
        }
        internal class MockEntity<TData> :MockEntity, IEntity<TData> {
            public TData Data => Mock.Func<TData>();
            public void SetData(TData d) => Mock.Action(d);
        }
        private class testClass :Entity<MeasureData> {
            public testClass(MeasureData d = null) : base(d) { }
        }
        protected override Entity<MeasureData> createObject() =>
            new testClass(GetRandom.ObjectOf<MeasureData>());
        [TestMethod] public void DataTest() => isReadOnly();
        [TestMethod] public void IsUnspecifiedTest() {
            Assert.IsTrue(new testClass().IsUnspecified);
            Assert.IsFalse(new testClass(GetRandom.ObjectOf<MeasureData>()).IsUnspecified);
        }
        [TestMethod] public void CanSetDataTest() {
            var d = GetRandom.ObjectOf<MeasureData>();
            obj = new testClass(d);
            Assert.AreNotSame(d, obj.Data);
            allPropertiesAreEqual(d, obj.Data);
        }
        [TestMethod] public void CanSetNullDataTest() {
            obj = new testClass();
            Assert.IsNotNull(obj.Data);
            Assert.IsTrue(obj.IsUnspecified);
        }
        [TestMethod] public void CantChangeDataElementsTest() {
            obj = new testClass(GetRandom.ObjectOf<MeasureData>());
            var d = obj.Data;
            obj.Data.Id = rndStr;
            obj.Data.Name = rndStr;
            obj.Data.Code = rndStr;
            obj.Data.Details = rndStr;
            obj.Data.ValidFrom = rndDt;
            obj.Data.ValidTo = rndDt;
            allPropertiesAreEqual(d, obj.Data);
        }
        [TestMethod] public void DetailsTest() => isReadOnly(obj.Data.Details);
        [TestMethod] public void CodeTest() => isReadOnly(obj.Data.Code);
        [TestMethod] public void NameTest() => isReadOnly(obj.Data.Name);
    }
}
