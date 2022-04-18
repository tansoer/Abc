using System.Linq;
using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;
using Abc.Infra.Quantities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Infra.Common {

    [TestClass] public class CrudRepoTests :AbstractTests<CrudRepo<Measure, MeasureData>, BaseRepo> {

        private MeasureData dataInDb;
        private MeasureData dataNotInDb;
        private Measure objInDb;
        private Measure objNotInDb;
        private string idNotInDb;

        private class testClass :CrudRepo<Measure, MeasureData> {
            public testClass(DbContext c, DbSet<MeasureData> s) :base(c, s) { }
            protected internal override Measure toDomainObject(MeasureData d) => MeasureFactory.Create(d);
        }

        protected override CrudRepo<Measure, MeasureData> createObject() {
            var options = new DbContextOptionsBuilder<QuantityDb>().UseInMemoryDatabase("TestDb").Options;
            var c = new QuantityDb(options);
            return new testClass(c, c.Measures);
        }

        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            idNotInDb = random<string>();
            dataInDb = random<MeasureData>();
            dataNotInDb = random<MeasureData>();
            objInDb = MeasureFactory.Create(dataInDb);
            objNotInDb = MeasureFactory.Create(dataNotInDb);
            areEqual(true, add(dataInDb));
        }

        [TestMethod] public void GetTest() => isSpecified(dataInDb);

        [TestMethod] public async Task GetAsyncTest() {
            var o = await obj.GetAsync(dataInDb.Id);
            Assert.IsFalse(o.IsUnspecified);
            Assert.IsInstanceOfType(o, typeof(Measure));
            allPropertiesAreEqual(dataInDb, o.Data);
        }

        [TestMethod] public void DeleteTest() {
            isSpecified(dataInDb);
            areEqual(true, delete(dataInDb));
            isUnspecified(dataInDb);
        }

        [TestMethod] public async Task DeleteAsyncTest() {
            isSpecified(dataInDb);
            areEqual(true, await deleteAsync(dataInDb));
            isUnspecified(dataInDb);
        }

        [TestMethod] public void AddTest() {
            isUnspecified(dataNotInDb);
            areEqual(true, add(dataNotInDb));
            isSpecified(dataNotInDb);
        }

        [TestMethod] public async Task AddAsyncTest() {
            isUnspecified(dataNotInDb);
            areEqual(true, await addAsync(dataNotInDb));
            isSpecified(dataNotInDb);
        }

        [TestMethod] public void UpdateTest() {
            isUnspecified(dataNotInDb);
            dataNotInDb.Id = dataInDb.Id;
            areEqual(true, update(dataNotInDb));
            isSpecified(dataNotInDb);
        }

        [TestMethod] public async Task UpdateAsyncTest() {
            isUnspecified(dataNotInDb);
            dataNotInDb.Id = dataInDb.Id;
            areEqual(true, await updateAsync(dataNotInDb));
            isSpecified(dataNotInDb);
        }

        [TestMethod] public void GetListTest() {
            for (var i = 0; i < GetRandom.UInt8(10, 30); i++)
                add(GetRandom.ObjectOf<MeasureData>());
            var l = obj.Get();
            Assert.AreEqual(obj.dbSet.Count(), l.Count);
        }

        [TestMethod] public async Task GetListAsyncTest() {
            for (var i = 0; i < GetRandom.UInt8(10, 30); i++)
                await addAsync(GetRandom.ObjectOf<MeasureData>());
            var l = await obj.GetAsync();
            areEqual(obj.dbSet.Count(), l.Count);
        }

        [TestMethod] public void DeleteErrorTest() {
            const string n = nameof(obj.Delete);
            bool f(string x) => obj.Delete(x);
            errorMsgTest(() => f(null), idIsNull(n));
            errorMsgTest(() => f(idNotInDb), itemNotFound(idNotInDb, n));
            errorMsgTest(() => f(dataInDb.Id), isException(n), true);
        }

        [TestMethod] public void DeleteAsyncErrorTest() {
            const string n = nameof(obj.DeleteAsync);
            async Task<bool> f(string x) => await obj.DeleteAsync(x);
            errorMsgTest(() => f(null), idIsNull(n));
            errorMsgTest(() => f(idNotInDb), itemNotFound(idNotInDb, n));
            errorMsgTest(() => f(dataInDb.Id), isException(n), true);
        }

        [TestMethod] public void UpdateErrorTest() {
            const string n = nameof(obj.Update);
            bool f(Measure x) => obj.Update(x);
            errorMsgTest(() => f(null), itemIsNull(n));
            errorMsgTest(() => f(objNotInDb), itemNotFound(objNotInDb.Id, n));
            errorMsgTest(() => f(objInDb), isException(n), true);
        }

        [TestMethod] public void UpdateAsyncErrorTest() {
            const string n = nameof(obj.UpdateAsync);
            async Task<bool> f(Measure x) => await obj.UpdateAsync(x);
            errorMsgTest(() => f(null), itemIsNull(n));
            errorMsgTest(() => f(objNotInDb), itemNotFound(objNotInDb.Id, n));
            errorMsgTest(() => f(objInDb), isException(n), true);
        }

        [TestMethod] public void AddErrorTest() {
            const string n = nameof(obj.Add);
            bool f(Measure x) => obj.Add(x);
            errorMsgTest(() => f(null), itemIsNull(n));
            errorMsgTest(() => f(objInDb), itemFound(objInDb.Id, n));
            errorMsgTest(() => f(objNotInDb), isException(n), true);
        }

        [TestMethod] public void AddAsyncErrorTest() {
            const string n = nameof(obj.AddAsync);
            async Task<bool> f(Measure x) => await obj.AddAsync(x);
            errorMsgTest(() => f(null), itemIsNull(n));
            errorMsgTest(() => f(objInDb), itemFound(objInDb.Id, n));
            errorMsgTest(() => f(objNotInDb), isException(n), true);
        }

        private void errorMsgTest(Func<bool> f, string msg, bool isException = false) {
            if (isException) obj.dbSet = null;
            f();
            isTrue(obj.ErrorMessage.StartsWith(msg),
                $"Expected <{msg}>; Actual <{obj.ErrorMessage}>");
        }

        private void errorMsgTest(Func<Task<bool>> f, string msg, bool isException = false) {
            if (isException) obj.dbSet = null;
            f();
            isTrue(obj.ErrorMessage.StartsWith(msg),
                $"Expected <{msg}>; Actual <{obj.ErrorMessage}>");
        }

        private static string idIsNull(string n) => string.Format(BaseRepo.idIsNullMsg, n);
        private static string itemNotFound(string id, string n) => string.Format(BaseRepo.itemNotFoundMsg, id, n);
        private static string itemIsNull(string n) => string.Format(BaseRepo.itemIsNullMsg, n);
        private static string itemFound(string id, string n) => string.Format(BaseRepo.itemFoundMsg, id, n);
        private static string isException(string n) => $"The method <{n}> has thrown the exception";
        private bool add(MeasureData d) => obj.Add(MeasureFactory.Create(d));
        private bool delete(MeasureData d) => obj.Delete(d.Id);
        private bool update(MeasureData d) => obj.Update(MeasureFactory.Create(d));
        private async Task<bool> addAsync(MeasureData d) => await obj.AddAsync(MeasureFactory.Create(d));
        private async Task<bool> deleteAsync(MeasureData d) => await obj.DeleteAsync(d.Id);
        private async Task<bool> updateAsync(MeasureData d) => await obj.UpdateAsync(MeasureFactory.Create(d));

        private void isSpecified(MeasureData d) {
            var o = obj.Get(d.Id);
            Assert.IsFalse(o.IsUnspecified);
            Assert.IsInstanceOfType(o, typeof(Measure));
            allPropertiesAreEqual(d, o.Data);
        }

        private void isUnspecified(MeasureData d) {
            var o = obj.Get(d.Id);
            Assert.IsTrue(o.IsUnspecified);
        }
    }
}
