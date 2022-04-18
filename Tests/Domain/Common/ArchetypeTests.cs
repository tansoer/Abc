using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Parties.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Abc.Tests.Domain.Common {
    [TestClass]
    public class ArchetypeTests : ClassTests<Archetype, object>{
        private class testClass:Archetype { }
        protected override Archetype createObject() => new testClass();
        [TestMethod] public void GetStringTest() {
            var s = random<string>();
            areEqual(Unspecified.String, Archetype.get((string)null));
            areEqual(s, Archetype.get(s));
        }
        [TestMethod] public void GetDecimalTest() {
            var s = random<decimal>();
            areEqual(Unspecified.Decimal, Archetype.get((decimal?)null));
            areEqual(s, Archetype.get(s));
        }
        [TestMethod] public void GetDoubleTest() {
            var s = random<double>();
            areEqual(Unspecified.Double, Archetype.get((double?)null));
            areEqual(s, Archetype.get(s));
        }
        [TestMethod] public void GetUintTest() {
            var s = random<uint>();
            areEqual(Unspecified.UInt, Archetype.get((uint?)null));
            areEqual(s, Archetype.get(s));
        }
        [TestMethod] public void GetIntegerTest() {
            var s = random<int>();
            areEqual(Unspecified.Integer, Archetype.get((int?)null));
            areEqual(s, Archetype.get(s));
        }
        [TestMethod] public void GetByteTest() {
            var s = random<byte>();
            areEqual(Unspecified.Byte, Archetype.get((byte?)null));
            areEqual(s, Archetype.get(s));
        }
        [TestMethod] public void GetBoolTest() {
            var s = random<bool>();
            areEqual(false, Archetype.get((bool?)null));
            areEqual(s, Archetype.get(s));
        }
        [TestMethod] public void GetDateTimeTest() {
            var s = random<DateTime>();
            areEqual(Unspecified.ValidFromDate, Archetype.get((DateTime?)null));
            areEqual(Unspecified.ValidToDate, Archetype.get(null, false));
            areEqual(s, Archetype.get(s));
        }
        [DataRow("random")] [DataRow("")] [DataRow(null)]
        [DataTestMethod] public void GetItemFromRepoTest(string s) {
            var isRandom = s == "random";
            var id = Archetype.nameOf<CountryData>(x => x.Id);
            var d = isRandom? new Country(random<CountryData>()): new Country();
            if (isRandom) Archetype.add<ICountriesRepo, Country>(d);
            var itemId = isRandom ? d.Id : s is null ? null : random<string>();
            
            var c = Archetype.item<ICountriesRepo, Country>(itemId);
            
            if (isRandom) allPropertiesAreEqual(d.Data, c.Data);
            else allPropertiesAreEqual(new Country().Data, c.Data, id);
        }
        [TestMethod] public void AddTest() => GetItemFromRepoTest("random");
        [TestMethod] public void NameOfTest() => GetItemFromRepoTest("random");
        [TestMethod] public void IsNullStringTest() {
            isTrue(Archetype.isNull((string)null));
            isTrue(Archetype.isNull(string.Empty));
            isTrue(Archetype.isNull(""));
            isTrue(Archetype.isNull("     "));
            isTrue(Archetype.isNull(Unspecified.String));
            isFalse(Archetype.isNull(random<string>()));
        }
        [TestMethod] public void IsNullIntTest() {
            isTrue(Archetype.isNull((uint?)null));
            isTrue(Archetype.isNull(Unspecified.UInt));
            isFalse(Archetype.isNull(random<uint?>()));
        }
        [TestMethod] public void IsNullDateTimeTest() {
            isTrue(Archetype.isNull((DateTime?)null));
            isTrue(Archetype.isNull(Unspecified.ValidFromDate));
            isTrue(Archetype.isNull(Unspecified.ValidToDate, false));
            isFalse(Archetype.isNull(Unspecified.ValidToDate));
            isFalse(Archetype.isNull(random<DateTime?>()));
        }
        [TestMethod] public void ToNumberTest() {
            var i = random<uint>();
            var u = uint.MaxValue;
            areEqual(i, Archetype.toNumber(i.ToString()));
            areEqual(u, Archetype.toNumber("-"+i.ToString()));
            areEqual(u, Archetype.toNumber(random<double>().ToString()));
            areEqual(u, Archetype.toNumber(random<string>()));
            areEqual(u, Archetype.toNumber(null));
        }
        [TestMethod] public void DeleteTest() {
            var d = new Country(random<CountryData>());
            Archetype.add<ICountriesRepo, Country>(d);
            var c = Archetype.item<ICountriesRepo, Country>(d.Id);
            allPropertiesAreEqual(d.Data, c.Data);
            Archetype.delete<ICountriesRepo, Country>(d);
            c = Archetype.item<ICountriesRepo, Country>(d.Id);
            var id = Archetype.nameOf<CountryData>(x => x.Id);
            allPropertiesAreEqual(new Country().Data, c.Data, id);
        }
        [TestMethod] public void GetItemFromListTest() {
            var d = new Country(random<CountryData>());
            var l = new List<Country>();
            var count = GetRandom.UInt8(5, 15);
            var idx = GetRandom.UInt8(1, count);
            for(var i = 0; i < count; i++) {
                if (idx == i + 1) l.Add(d);
                else l.Add(new Country(random<CountryData>()));
            }
            var c = Archetype.item(l, x => x.Name == d.Name);
            allPropertiesAreEqual(d.Data, c.Data);
        }
        [TestMethod] public void UpdateTest() {
            var d = new Country(random<CountryData>());
            Archetype.add<ICountriesRepo, Country>(d);
            var c = Archetype.item<ICountriesRepo, Country>(d.Id);
            allPropertiesAreEqual(d.Data, c.Data);
            var dd = random<CountryData>();
            dd.Id = d.Id;
            var d1 = new Country(dd);
            Archetype.update<ICountriesRepo, Country>(d1);
            c = Archetype.item<ICountriesRepo, Country>(d.Id);
            allPropertiesAreEqual(d1.Data, c.Data);
        }
        [TestMethod] public void GetItemsFromListTest() {
            var l = new List<Country>();
            var count = GetRandom.UInt8(20, 40);
            var s = random<string>();
            var idx = GetRandom.UInt8(1, count);
            for (var i = 0; i < count; i++) {
                var d = random<CountryData>();
                if (i < idx) d.Name = s;
                l.Add(new Country(d));
            }
            var c = Archetype.list(l, x => x.Name == s);
            areEqual((int)idx, c.Count);
        }
        [TestMethod] public void GetTypesFromListTest() {
            var l = new List<object>();
            var count = GetRandom.UInt8(20, 40);
            var idx = GetRandom.UInt8(1, count);
            for (var i = 0; i < count; i++) {
                var d = random<CountryData>();
                if (i < idx) l.Add(new Country(d));
                else l.Add(random<string>());
            }
            var c = Archetype.list<Country, object>(l);
            areEqual((int)idx, c.Count);
        }
        [TestMethod] public void GetTransformedFromListTest() {
            var l = new List<Country>();
            var count = GetRandom.UInt8(20, 40);
            for (var i = 0; i < count; i++) {
                var d = random<CountryData>();
                l.Add(new Country(d));
            }
            var c = Archetype.list(l, x => x.Name);
            areEqual(l.Count, c.Count);
            foreach(var n in c) {
                var i = Archetype.item(l, x => x.Name == n);
                areEqual(n, i.Name);
            }
        }
        [TestMethod] public void GetItemsFromRepoTest() {
            var count = GetRandom.UInt8(20, 40);
            var s = random<string>();
            var idx = GetRandom.UInt8(1, count);
            for (var i = 0; i < count; i++) {
                var d = random<CountryData>();
                if (i < idx) d.Name = s;
                Archetype.add<ICountriesRepo, Country>(new Country(d));
            }
            var c = Archetype.list<ICountriesRepo, Country>("Name", s);
            areEqual((int)idx, c.Count);
            foreach (var n in c) {
                areEqual(s, n.Name);
            }
        }
    }
}
