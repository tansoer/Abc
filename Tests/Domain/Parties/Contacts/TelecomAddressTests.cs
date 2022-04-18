using System;
using System.Collections.Generic;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Contacts;
using Abc.Infra.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Contacts {
    [TestClass]
    public class TelecomAddressTests : SealedTests<TelecomAddress, PartyContact<PartyContactData>> {
        private DeviceRegistrationsRepo devices;
        private PartyDb db;
        protected override TelecomAddress createObject() {
            base.createObject();
            var d = GetRandom.ObjectOf<PartyContactData>();
            d.CityOrAreaCode = GetRandom.UInt16().ToString();
            d.Code = GetRandom.UInt16().ToString();
            d.Name = GetRandom.UInt16().ToString();
            d.NationalDirectDialingPrefix = GetRandom.UInt16().ToString();
            d.RegionOrStateOrCountryCode = GetRandom.UInt16().ToString();
            d.ContactType = ContactType.Telecom;
            return new TelecomAddress(d);
        }
        [TestCleanup]
        public override void TestCleanup() {
            removeAll(devices?.dbSet, db);
            devices = null;
            db = null;
            base.TestCleanup();
        }
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            devices = GetRepo.Instance<IDeviceRegistrationsRepo>() as DeviceRegistrationsRepo;
            db = devices?.db as PartyDb;
        }
        [TestMethod]
        public void RegistrationsTest() {
            var count = GetRandom.UInt8(10, 30);
            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<DeviceRegistrationData>();
                if (i % 4 == 0) d.TelecomDeviceId = obj.Id;
                devices.Add(new DeviceRegistration(d));
            }
            var t = isReadOnly() as IReadOnlyList<DeviceRegistration>;
            Assert.IsNotNull(t);
            Assert.AreEqual(Math.Ceiling(count / 4.0), t.Count);
        }
        [TestMethod] public void CountryCodeTest() => isReadOnly(obj.Data.RegionOrStateOrCountryCode);
        [TestMethod] public void NationalDirectDialingPrefixTest() => isReadOnly(obj.Data.NationalDirectDialingPrefix);
        [TestMethod] public void AreaCodeTest() => isReadOnly(obj.Data.CityOrAreaCode);
        [TestMethod] public void NumberTest() => isReadOnly(obj.Data.Name);
        [TestMethod] public void ExtensionTest() => isReadOnly(obj.Data.Code);
        [TestMethod] public void DeviceTest() => isReadOnly(obj.Data.Device);
        [TestMethod] public void ToStringTest() => Assert.AreEqual(toPhoneNumber(obj), obj.ToString());
        private static string toPhoneNumber(TelecomAddress a) =>
            $"+{a.CountryCode} ({a.NationalDirectDialingPrefix}){a.AreaCode} {a.Number} ext.{a.Extension}";
    }
}
