using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests {
    public abstract class TestsData: TestsBase {

        [TestCleanup] public override void TestCleanup() => base.TestCleanup();
        protected internal static void add<TRepo, TObj>(TObj o)
            where TObj : IBaseEntity where TRepo : IRepo<TObj> => getRepo<TRepo>().Add(o);
        protected internal static void update<TRepo, TObj>(TObj o)
            where TObj : IBaseEntity where TRepo : IRepo<TObj> => getRepo<TRepo>().Update(o);
        protected internal static async Task updateAsync<TRepo, TObj>(TObj o)
           where TObj : IBaseEntity where TRepo : IRepo<TObj> => await getRepo<TRepo>().UpdateAsync(o);
        protected internal static async Task addAsync<TRepo, TObj>(TObj o)
           where TObj : IBaseEntity where TRepo : IRepo<TObj> => await getRepo<TRepo>().AddAsync(o);
        protected internal static void delete<TRepo, TObj>(TObj o)
            where TRepo : IRepo<TObj> where TObj : IBaseEntity => getRepo<TRepo>().Delete(o?.Id);
        protected internal static async Task deleteAsync<TRepo, TObj>(TObj o)
            where TRepo : IRepo<TObj> where TObj : IBaseEntity => await getRepo<TRepo>().DeleteAsync(o?.Id);
        protected static TR getRepo<TI, TR>() where TR : class, TI => getRepo<TI>() as TR;
        protected static TI getRepo<TI>() => GetRepo.Instance<TI>();
        protected static PartySignature getSignature(string signatureId = null, string relatedEntityId = null) {
            var sd = GetRandom.ObjectOf<PartySignatureData>();
            if (signatureId is not null) sd.Id = signatureId;
            if (relatedEntityId is not null) sd.Id = relatedEntityId;
            sd.ValidFrom = GetRandom.DateTime(DateTime.Now.AddYears(-10), DateTime.Now);
            sd.ValidTo = null;
            return new PartySignature(sd);
        }
        protected internal static void removeAll<TData>(DbSet<TData> set, DbContext db) where TData : class, new() {
            if (set is null) return;
            if (db is null) return;
            set.RemoveRange(set);
            db.SaveChanges();
            areEqual(0, set.Count(), typeof(TData).Name);
        }
        private static async Task<TData> testAddItemAsync<TData, TObject, TRepo>(
            string id, Func<TData> getData, Func<TData, TObject> toObject)
            where TData : BaseData, new()
            where TObject : IBaseEntity<TData>
            where TRepo : IRepo<TObject> {
            var d = random<TData>();
            await addAsync<TRepo, TObject>(toObject(d));
            isNotNull(getData());
            allPropertiesAreEqual(getData(), new TData(), "Id");
            d.Id = id;
            return d;
        }
        protected static async Task testItemAsync<TData, TObject, TRepo>(string id, Func<TData> getData,
            Func<TData, TObject> toObject)
            where TData : BaseData, new()
            where TObject : IBaseEntity<TData>
            where TRepo : IRepo<TObject> {
            var o = toObject(null);
            isNotNull(o);
            var d = await testAddItemAsync<TData, TObject, TRepo>(id, getData, toObject);
            await testItemAsync<TData, TObject, TRepo>(d, getData, toObject);
        }
        protected static async Task testItemAsync<TData, TObject, TRepo>(
            TData d, Func<TData> getData, Func<TData, TObject> toObject)
            where TData : BaseData, new()
            where TObject : IBaseEntity<TData>
            where TRepo : IRepo<TObject> {
            await addAsync<TRepo, TObject>(toObject(d));
            var y = getData();
            allPropertiesAreEqual(d, y);
        }
        protected static async Task testListAsync<TDetail, TData, TRepo>(
            object obj, string name, Action<TData> setId, Func<TData, TDetail> toObject)
            where TDetail : IBaseEntity
            where TRepo : IRepo<TDetail> {
            var t = isReadOnly(obj, name) as IReadOnlyList<TDetail>;
            isNotNull(t);
            areEqual(0, t?.Count, "Count before insert");
            var valuesCount = GetRandom.UInt8(30, 50);
            for (var i = 0; i < valuesCount; i++) {
                var d = random<TData>();
                if (i % 4 == 0) setId(d);
                await addAsync<TRepo, TDetail>(toObject(d));
            }
            t = isReadOnly(obj, name) as IReadOnlyList<TDetail>;
            areEqual((int)Math.Ceiling(valuesCount / 4.0), t?.Count, "Count after insert");
        }
        protected async Task testListAsync<TObject, TData, TRepo>(
            Action<TData> setId, Func<TData, TObject> toObject, bool firstCharToLowerCaseInName = false)
            where TObject : IBaseEntity
            where TRepo : IRepo<TObject> {
            var n = getNameAfter(nameof(testListAsync));
            if (firstCharToLowerCaseInName) n = firstCharToLower(n);
            await testListAsync<TObject, TData, TRepo>(objUnderTests, n, setId, toObject);
        }
        protected static async Task testPartyAttributeAsync
            <TData, TObject, TRepo>(string id, Func<TData> getData,
            Func<TData, TObject> toObject)
            where TData : PartyAttributeData, new()
            where TObject : IEntity<TData>
            where TRepo : IRepo<TObject> {
            var d = await testAddItemAsync<TData, TObject, TRepo>(id, getData, toObject);
            await testItemAsync<TData, TObject, TRepo>(d, getData, toObject);
        }
        protected void testRelatedList<TObject, TData, TRelatedObject, TRepo>(
            Func<IReadOnlyList<TObject>> getList,
            Func<IReadOnlyList<TRelatedObject>> getRelatedList,
            Func<TData, TRelatedObject, TObject> getObject,
            Func<Task> relatedTest,
            Func<TObject, TRelatedObject, bool> isThis)
            where TRepo : IRepo<TObject> {
            var n = getNameAfter(nameof(testRelatedList));
            isReadOnly(objUnderTests, n);
            areEqual(0, getList().Count);
            relatedTest().GetAwaiter().GetResult();
            var repo = GetRepo.Instance<TRepo>();
            var relatedList = getRelatedList();
            foreach (var e in relatedList) {
                var d = GetRandom.ObjectOf<TData>();
                var o = getObject(d, e);
                isTrue(repo.Add(o));
            }
            var list = getList();
            areNotEqual(0, list.Count);
            areEqual(relatedList.Count, list.Count);
            foreach (var r in relatedList) {
                var t = list.FirstOrDefault(x => isThis(x, r));
                isNotNull(t);
            }
        }
    }
}
