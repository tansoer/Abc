using Abc.Aids.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Common;
using Abc.Domain.Classifiers;
using Abc.Infra.Classifiers;
using Abc.Infra.Quantities;
using AngleSharp.Html.Dom;
using AngleSharp.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Abc.Facade.Common;

namespace Abc.Tests.Soft {

    public abstract class BaseAcceptanceTests<TView, TData, TContext> : BaseControlsTests<TView, TData, TContext>
        where TContext : DbContext where TData : BaseData where TView: BaseView{
        protected ClassifierDb classifiersDb;
        protected QuantityDb quantityDb;
        [TestInitialize] public override void TestInitialize() {
            classifiersDb = getContext<ClassifierDb>();
            quantityDb = getContext<QuantityDb>();
            base.TestInitialize();
        }
        protected static void addClassifier(string id, ClassifierType t) {
            var d = random<ClassifierData>();
            d.Id = id;
            d.ClassifierType = t;
            add<IClassifiersRepo, IClassifier>(ClassifierFactory.Create(d));
        }
        [TestMethod] public virtual void CanClickBackToListLinkTest() {
            htmlDocument = sendRequest(pageUrl());
            var e = htmlDocument.FindAnchorElement("a[id='backToList']");
            sendRequest(e.Href);
            var expected = HtmlTag.IndexForm(baseUrl());
            if (pageHtml.Contains(expected)) return;
            Assert.AreEqual(pageHtml, expected);
        }
        [TestMethod] public virtual void HasLinkInIndexPageTest() {
            sendRequest(indexPageUrl());
            if (pageHtml.Contains(pageUrl())) return;
            Assert.AreEqual(pageHtml, pageUrl());
        }
        [TestMethod] public void CanClickNavButtonsTest() {
            if (!isIndexPage) return;
            var tests = new NavButtonsAcceptanceTests(addToDatabase,
                pageUrl, baseUrl, () => numberOfPages, sendRequest);
            tests.DoBeforeValidation += addButtonsFixedFilterAndValue;
            tests.DoTest();
        }
        protected virtual void addButtonsFixedFilterAndValue(ref string html) { }
        protected virtual void addHeadersFixedFilterAndValue(ref string html) { }
        protected abstract IEnumerable<Expression<Func<TView, object>>> indexPageColumns { get; }
        [TestMethod] public virtual void CanClickTableHeadersTest() {
            if (!isIndexPage) return;
            var tests = new TableHeadersAcceptanceTests<TView>(addToDatabase,
                pageUrl, baseUrl, sendRequest, indexPageColumns);
            tests.DoBeforeValidation += addHeadersFixedFilterAndValue;
            tests.DoTest();
        }
        [TestMethod] public void CanClickDeleteButtonTest() => canClickDeleteButtonTest();
        protected virtual void canClickDeleteButtonTest() {
            if (!isDeletePage) return;
            addToDatabase();
            var id = getItemId(item);
            db = getContext<TContext>();
            var x = db.Find<TData>(toComposedId(id));
            Assert.AreEqual(id, getItemId(x));
            htmlDocument = sendRequest(pageUrl());
            var form = htmlDocument.FindFormElement("form[id='deleteForm']");
            var button = htmlDocument.FindInputElement("input[id='deleteButton']");
            var m = HtmlDoc.ComposeRequestMessage(form, button);
            htmlDocument = sendRequest(m);
            var expected = HtmlTag.IndexForm(baseUrl());
            db = getContext<TContext>();
            x = db.Find<TData>(toComposedId(id));
            Assert.IsNull(x);
            if (pageHtml.Contains(expected)) return;
            Assert.AreEqual(pageHtml, expected);
        }
        [TestMethod] public void CanClickEditButtonInDetailsTest() => canClickEditButtonInDetailsTest();
        protected virtual void canClickEditButtonInDetailsTest() {
            if (!isDetailsPage) return;
            var url = pageUrl();
            htmlDocument = sendRequest(url);
            var e = htmlDocument.FindAnchorElement("a[id='editButton']");
            htmlDocument = sendRequest(e.Href);
            Assert.IsNotNull( htmlDocument.FindFormElement("form[id='editForm']"));
            Assert.IsNotNull(htmlDocument.FindInputElement("input[id='saveButton']"));
        }
        [TestMethod] public virtual void CanClickCreateButtonTest() => canClickCreateButtonTest();
        protected virtual void canClickCreateButtonTest(Action<TData> changeItem = null) {
            if (!isCreatePage) return;
            addToDatabase();
            item = randomItem();
            addRelatedItems(item);
            changeItem?.Invoke(item);
            var id = getItemId(item);
            db.SaveChanges();
            var x = db.Find<TData>(toComposedId(id));
            Assert.IsNull(x);
            areRelatedItemsInDb(db, item);
            htmlDocument = sendRequest(pageUrl());
            var form = htmlDocument.FindFormElement("form[id='createForm']");
            var button = htmlDocument.FindInputElement("input[id='createButton']");
            var m = ComposeRequestMessage(form, button, item); ;
            htmlDocument = sendRequest(m);
            var expected = HtmlTag.IndexForm(baseUrl());
            db = getContext<TContext>();
            x = db.Find<TData>(toComposedId(id));
            Assert.IsNotNull(x);
            Assert.AreEqual(id, getItemId(x));
            validateItems(item, x);
            if (pageHtml.Contains(expected)) return;
            Assert.AreEqual(expected, pageHtml);
        }
        protected virtual void areRelatedItemsInDb(TContext c, TData d) { }
        protected virtual void validateItems(TData d1, TData d2)
            => allPropertiesAreEqual(d1, d2);
        [TestMethod] public virtual void CanClickEditButtonTest() => canClickEditButtonTest();
        protected virtual void canClickEditButtonTest() {
            if (!isEditPage) return;
            addToDatabase();
            var id = getItemId(item);
            addRelatedItems(item);
            db.SaveChanges();
            var x = db.Find<TData>(toComposedId(id));
            Assert.AreEqual(id, getItemId(x));
            item = randomItem();
            addRelatedItems(item);
            correctOriginal(x, item);
            setItemId(item, id);
            htmlDocument = sendRequest(pageUrl());
            var form = htmlDocument.FindFormElement("form[id='editForm']");
            var button = htmlDocument.FindInputElement("input[id='saveButton']");
            var m = ComposeRequestMessage(form, button, item);
            htmlDocument = sendRequest(m);
            var expected = HtmlTag.IndexForm(baseUrl());
            db = getContext<TContext>();
            x = db.Find<TData>(toComposedId(id));
            Assert.IsNotNull(x);
            Assert.AreEqual(id, getItemId(x));
            validateItems(item, x);
            if (pageHtml.Contains(expected)) return;
            Assert.AreEqual(expected, pageHtml);
        }
        protected virtual TData correctOriginal(TData oldItem, TData newItem) => oldItem;
        protected virtual HttpRequestMessage ComposeRequestMessage(
            IHtmlFormElement form, IHtmlInputElement button, TData d) 
            => HtmlDoc.ComposeRequestMessage(form, button, toView(d));
        protected abstract TView toView(TData d);
        protected virtual void addRelatedItems(TData d) { }
        [TestMethod] public override void IsTested() => isCorrectPageName();
        [TestMethod] public override void IsSpecifiedClassTested() {
            isCorrectContext();
            isCorrectItem();
        }
        protected virtual void isCorrectPageName() {
            var n = baseClassName().ToLower();
            var title = pageTitle().ToLower();
            Assert.AreEqual(title, n);
        }
        protected string pageTitle() {
            htmlDocument = sendRequest(pageUrl());
            var header = htmlDocument.FindElement("h1");
            var n = header.TextContent.Replace("&", "and").RemoveSpaces();
            n = performTitleCorrection(n);

            return n;
        }
        protected virtual string performTitleCorrection(string n) => n[..^1];
        private void isCorrectItem() {
            var n = baseClassName();
            var itemName = item.GetType().Name;
            Assert.IsTrue(itemName.StartsWith(n), $"Not testing {n}");
        }
        protected virtual string baseClassName() {
            var n = GetType().BaseType?.Name;
            n = n?.ReplaceFirst("Base", string.Empty);
            n = n?.ReplaceFirst("sTests", string.Empty);
            return n;
        }
        protected virtual void isCorrectContext() {
            var n = baseBaseClassName();
            var contextName = db.GetType().Name;
            Assert.IsTrue(contextName.StartsWith(n), $"Not testing {n}");
        }
        private string baseBaseClassName() {
            var s = GetType().BaseType?.BaseType?.Name;
            s = s?.ReplaceFirst("Base", string.Empty);
            s = s?[..s.IndexOf("Tests", StringComparison.Ordinal)];
            return s;
        }
        protected void clickNavigationLink(string linkElementId, string resultUrl) {
            htmlDocument = sendRequest(pageUrl());
            var e = htmlDocument.FindAnchorElement($"a[id='{linkElementId}']");
            sendRequest(e.Href);
            var expected = HtmlTag.IndexForm(resultUrl);
            if (pageHtml.Contains(expected)) return;
            Assert.AreEqual(pageHtml, expected);
        }
        protected void clear<T>(DbSet<T> set, DbContext c = null) where T : class {
            var l = set.ToList();
            set.RemoveRange(l);
            c ??= db;
            c.SaveChanges();
            Assert.AreEqual(0, set.Count());
        }
        protected void addRandomRecord<T>(string id) where T : EntityBaseData =>
            addRandomRecord<T>(d => d.Id = id);
        protected void addRandomRecord<T>(Action<T> setIds) where T : EntityBaseData {
            var t = GetRandom.ObjectOf<T>();
            setIds(t);
            addToDatabase(t);
        }
        protected internal static string selectedItemName(IEnumerable<SelectListItem> list, string id) {
            if (list is null) return Word.Unspecified;
            foreach (var m in list) if (m.Value == id) return m.Text;
            return Word.Unspecified;
        }
    }
}
