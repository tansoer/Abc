using System;
using System.Collections.Generic;
using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Names;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties {

    [TestClass]
    public class OrganizationTests : SealedTests<Organization, Party<OrganizationName>> {

        internal ClassifierData dC;
        protected override Organization createObject() {
            base.createObject();
            var d = GetRandom.ObjectOf<PartyData>();
            d.PartyType = PartyType.Organization;
            addOrganizationType(d.OrganizationTypeId);
            return new Organization(d);
        }

        private void addOrganizationType(string typeId) {
            var r = GetRepo.Instance<IClassifiersRepo>();
            dC = GetRandom.ObjectOf<ClassifierData>();
            dC.ClassifierType = ClassifierType.Organization;
            dC.Id = typeId;
            r.Add(new OrganizationType(dC));
        }
        [TestMethod]
        public void NamesTest() {
            var count = GetRandom.UInt8(10, 20);

            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<PartyNameData>();
                if (i % 4 == 0) d.PartyId = obj.Id;
                d.PartyType = PartyType.Organization;
                GetRepo.Instance<IPartyNamesRepo>().Add(PartyNameFactory.Create(d));
            }

            var t = isReadOnly(obj, nameof(obj.Names)) as IReadOnlyList<OrganizationName>;
            Assert.IsNotNull(t);
            Assert.AreEqual(Math.Ceiling((decimal) count / 4), t.Count);
        }
        [TestMethod] public void typeIdTest() => isReadOnly(obj.Data.OrganizationTypeId);
        [TestMethod] public void TypeTest() => validateType(isReadOnly(obj, nameof(obj.Type)));
        private static void validateType(object o) {
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(OrganizationType));
            Assert.IsFalse(((OrganizationType) o).IsUnspecified);
        }
        [TestMethod] public void HasTypeTest() => allPropertiesAreEqual(dC, obj.Type.Data);

    }
}
