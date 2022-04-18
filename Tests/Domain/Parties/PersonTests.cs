using System;
using System.Collections.Generic;
using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Names;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties {
    [TestClass]
    public class PersonTests : SealedTests<Person, Party<PersonName>> {

        protected override Person createObject() {
            base.createObject();
            var d = GetRandom.ObjectOf<PartyData>();
            d.PartyType = PartyType.Person;
            return new Person(d);
        }

        [TestMethod]
        public void NamesTest() {
            var count = GetRandom.UInt8(10, 20);

            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<PartyNameData>();
                if (i % 4 == 0) d.PartyId = obj.Id;
                d.PartyType = PartyType.Person;
                GetRepo.Instance<IPartyNamesRepo>().Add(PartyNameFactory.Create(d));
            }

            var t = isReadOnly() as IReadOnlyList<PersonName>;
            Assert.IsNotNull(t);
            Assert.AreEqual(Math.Ceiling((decimal) count / 4), t.Count);
        }
        [TestMethod] public void GenderTest() => isReadOnly(obj.Data.Gender);
        [TestMethod]
        public void EthnicityTest() {
            var actual = isReadOnly(obj, nameof(obj.Ethnicity));
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(IReadOnlyList<PersonEthnicity>));
        }
        [TestMethod]
        public void BodyMetricsTest() {
            var actual = isReadOnly(obj, nameof(obj.BodyMetrics));
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(IReadOnlyList<IBodyMetric>));
        }

        [TestMethod] public void DateOfBirthTest() => isReadOnly(obj.Data.ValidFrom);

    }
}
