using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Products;
using Abc.Facade.Parties;
using Abc.Facade.Products;
using Abc.Infra.Parties;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {

    [TestClass]
    public class BatchCheckedByViewFactoryTests : ViewFactoryTests<BatchCheckedByViewFactory, 
        BatchCheckedByData, BatchCheckedBy, BatchCheckedByView> {

        protected override void evaluateData(BatchCheckedByView v, BatchCheckedByData d) 
            => allPropertiesAreEqual(d, v, nameof(v.CheckedBy), nameof(v.Address), nameof(v.EmailAddress), nameof(v.PhoneNumber));

        [TestMethod]
        public void CreateViewHasSignatureTest() {
            var d = GetRandom.ObjectOf<BatchCheckedByData>();

            var i = GetRepo.Instance<IPartySignaturesRepo>();
            var r = i as PartySignaturesRepo;
            var s = GetRandom.ObjectOf<PartySignatureData>();
            s.Id = d.PartySignatureId;
            r.db.Add(s);
            r.db.SaveChanges();
            var v = new BatchCheckedByViewFactory().Create(new BatchCheckedBy(d));
            allPropertiesAreEqual(d, v);
            areEqual(Unspecified.String, v.Address);
            areEqual(Unspecified.String, v.CheckedBy);
            areEqual(Unspecified.String, v.EmailAddress);
            areEqual(Unspecified.String, v.PhoneNumber);
        }
        [TestMethod]

        public void CreateViewHasSummaryTest() {
            var d = GetRandom.ObjectOf<BatchCheckedByData>();

            var i = GetRepo.Instance<IPartySignaturesRepo>();
            var r = i as PartySignaturesRepo;
            var s = GetRandom.ObjectOf<PartySignatureData>();
            var u = GetRandom.ObjectOf<PartySummaryData>();
            u.RoleInOrder = Abc.Data.Orders.PartyRoleInOrder.Unspecified;
            u.Id = s.PartySummaryId;
            s.Id = d.PartySignatureId;
            r.db.Add(s);
            r.db.Add(u);
            r.db.SaveChanges();

            var v = new BatchCheckedByViewFactory().Create(new BatchCheckedBy(d));

            allPropertiesAreEqual(d, v);
            areEqual(u.Address, v.Address);
            areEqual(u.Name, v.CheckedBy);
            areEqual(u.EmailAddress, v.EmailAddress);
            areEqual(u.PhoneNumber, v.PhoneNumber);
        }
    }
}