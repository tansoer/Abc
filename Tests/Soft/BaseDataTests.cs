using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Data.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft {
    public abstract class BaseDataTests<TData, TContext> :BasePagesTests<TData>
        where TContext : DbContext where TData : BaseData {
        protected TContext db;
        protected byte numberOfPages;
        protected int numberOfItems;
        protected byte numberOfItemsInLastPage;
        protected byte pageSize = Abc.Infra.Constants.DefaultPageSize;
        protected List<string> Editors;
        protected List<string> HiddenInputs;
        [TestInitialize] public virtual void TestInitialize() {
            db = getContext<TContext>();
            doOnCreated(db);
            item = randomItem();
            addToDatabase(item);
        }
        protected virtual void doOnCreated(TContext c) { }
        protected void addToDatabase() {
            numberOfPages = GetRandom.UInt8(3, 10);
            numberOfItemsInLastPage = GetRandom.UInt8(2, pageSize);
            numberOfItems = (numberOfPages - 1) * pageSize + numberOfItemsInLastPage;
            for (var i = 1; i < numberOfItems; i++) addToDatabase(randomItem());
        }
        protected void addToDatabase<T>(T d) {
            db.Add(d);
            db.SaveChanges();
        }
        protected void addItem<T>(Action<T> method) {
            var t = random<T>();
            method(t);
            addToDatabase(t);
        }
        protected virtual TData randomItem() {
            var d = (TData)GetRandom.ObjectOf(typeof(TData));
            d.ValidFrom = GetRandom.DateTime(DateTime.Now.AddYears(-10), DateTime.Now).Date;
            d.ValidTo = GetRandom.DateTime(DateTime.Now, DateTime.Now.AddYears(10)).Date;
            d.Timestamp = System.Text.Encoding.Default.GetBytes(Guid.NewGuid().ToString()[^8..]);
            return d;
        }
        [TestMethod] public virtual void CanDisplayDataTest() {
            htmlDocument = sendRequest(pageUrl());
            if (isDetailsPage) dataInDetailsPage();
            if (isDeletePage) dataInDeletePage();
            if (isEditPage || isCreatePage) {
                setEditorsAndHiddenInputs();
                dataHiddenInPage();
                if (isEditPage) dataInEditPage();
                if (isCreatePage) dataInCreatePage();
                if (Editors.Count != 0) Assert.Inconclusive($"There are {Editors.Count} untested fields. Starting with {Editors[0]}");
                if (HiddenInputs.Count != 0) Assert.Inconclusive($"There are {HiddenInputs.Count} untested hidden inputs. Starting with {HiddenInputs[0]}");
            }
        }
        protected virtual void dataInDeletePage() => dataInDetailsPage();
        protected virtual void dataInCreatePage() => Assert.Inconclusive();
        protected virtual void dataInEditPage() => Assert.Inconclusive();
        protected virtual void dataHiddenInPage() => Assert.Inconclusive();
        protected virtual void dataInDetailsPage() => Assert.Inconclusive();
        [TestMethod] public void CanDeleteDataTest() {
            addToDatabase();
            var id = getItemId(item);
            var x = db.Find<TData>(toComposedId(id));
            Assert.IsNotNull(x);
            Assert.AreEqual(getItemId(x), id);
            db.Remove(item);
            db.SaveChanges();
            x = db.Find<TData>(toComposedId(id));
            Assert.IsNull(x);
        }
        [TestMethod] public void CanCreateDataTest() {
            addToDatabase();
            item = randomItem();
            var id = getItemId(item);
            var x = db.Find<TData>(toComposedId(id));
            Assert.IsNull(x);
            db.Add(item);
            db.SaveChanges();
            x = db.Find<TData>(toComposedId(id));
            Assert.AreEqual(getItemId(x), id);
            Assert.IsNotNull(x);
        }
        protected static object[] toComposedId(string s) {
            var l = new List<object>();
            while (s.Contains('.')) {
                l.Add(s.GetHead());
                s = s.GetTail();
            }
            l.Add(s);
            return l.ToArray();
        }
        [TestMethod] public void CanEditDataTest() {
            addToDatabase();
            var id = getItemId(item);
            var x = db.Find<TData>(toComposedId(id));
            Assert.IsNotNull(x);
            allPropertiesAreEqual(x, item);
            changeValuesExceptId(item);
            notEqualProperties(x, item);
            Assert.AreEqual(getItemId(x), getItemId(item));
            db.Update(item);
            db.SaveChanges();
            x = db.Find<TData>(toComposedId(id));
            Assert.IsNotNull(x);
            allPropertiesAreEqual(x, item);
        }
        protected virtual void changeValuesExceptId(TData data) {
            var id = data.Id;
            data = random<TData>();
            data.Id = id;
        }
        protected static List<SelectListItem> createSelectList<T>(DbSet<T> dbSet, Func<T, bool> condition = null) where T : EntityBaseData
            => condition is null ?
                dbSet.Select(c => new SelectListItem(c.Name, c.Id)).ToList() :
                dbSet.Where(condition).Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected void setEditorsAndHiddenInputs() {
            Editors = getEditorsWithHeaders().ToList();
            HiddenInputs = getHiddenInputs().ToList();
        }
        private IEnumerable<string> getEditorsWithHeaders() {
            var result = getEditorHeaders().ToList();
            var editors = getEditors().ToList();
            for (int i = 0; i < editors.Count; i++) result[i] += editors[i];
            return result;
        }
        private IEnumerable<string> getEditorHeaders() =>
            htmlDocument.All.Where(x => x.OuterHtml.StartsWith("<dd class=\"col-sm-2\"")).Select(x => x.OuterHtml);
        private IEnumerable<string> getEditors()
            => htmlDocument.All.Where(x => x.OuterHtml.StartsWith("<dd class=\"col-sm-10\"") && x.OuterHtml.Contains("form-control"))
                .Select(x => x.OuterHtml);
        private IEnumerable<string> getHiddenInputs() => htmlDocument.All
            .Where(x => x.OuterHtml.StartsWith("<input") && x.OuterHtml.Contains("hidden") && !x.OuterHtml.Contains("Token"))
            .Select(x => x.OuterHtml);
    }
}
