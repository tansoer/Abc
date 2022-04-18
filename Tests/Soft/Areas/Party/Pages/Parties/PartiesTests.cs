using Abc.Aids.Enums;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using AngleSharp.Html.Dom;
using AngleSharp.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;

namespace Abc.Tests.Soft.Areas.Party.Pages.Parties {
    public abstract class PartiesTests :BasePartyTests<PartyView, PartyData> {
        protected PartyType? partyType;
        protected List<SelectListItem> organizationTypes => createSelectList(classifiersDb.Classifiers,
            x => x.ClassifierType == ClassifierType.Organization);
        protected List<SelectListItem> contactUsages => createSelectList(db.PartyContactUsages);
        [BindProperty] protected PartyNameView Name => getName();
        private PartyNameView getName() {
            var d = db.PartyNames.FirstOrDefault(x => x.PartyId == item.Id) ?? random<PartyNameData>();
            d.PartyId = item.Id;
            d.PartyType = isPerson() ? PartyType.Person : PartyType.Organization;
            return new PartyNameViewFactory().Create(isPerson() ? new PersonName(d) : new OrganizationName(d));
        }
        protected override string baseUrl() => PartyUrls.Parties;
        protected override string performTitleCorrection(string title) => title[0..^3];
        protected override void addRelatedItems(PartyData d) {
            if (d is null) return;
            addClassifier(d.OrganizationTypeId, ClassifierType.Organization);
            addContactUsage(d.Id);
            addPartyName(d.Id);
        }
        protected override IEnumerable<Expression<Func<PartyView, object>>> indexPageColumns =>
            new Expression<Func<PartyView, object>>[] {
                x => x.PartyType,
                x => x.Name,
                x => x.Description
            };
        private bool isPerson() => partyType == PartyType.Person;
        private bool isOrganization() => partyType == PartyType.Organization;
        protected override PartyView  toView(PartyData d) {
            var o = PartyFactory.Create(d);
            var v = new PartyViewFactory().Create(o);
            return v;
        }
        protected override void dataInDetailsPage() {
            var v = toView(item) as PartyView;
            canView(v, x => x.PartyType);
            if (v?.IsPerson() ?? false) {
                canView(v, x => x.Name, Unspecified.String);
                canView(v, x => x.ValidFrom);
                canView(v, x => x.Gender);
                canView(v, x => x.Description);
            }
            else if (v?.IsOrganization() ?? false) {
                canView(v, x => x.OrganizationTypeId, v.OrganizationTypeId);
                canView(v, x => x.Name);
                canView(v, x => x.Description);
            }
        }
        protected override void dataInEditPage() {
            var v = toView(item) as PartyView;
            if (v?.IsPerson() ?? false) {
                canEdit(null, x => Name.Name, "Family Name", true);
                canEdit(null, x => Name.GivenName, "First Name");
                canEdit(null, x => Name.MiddleName, "Middle Name");
                canEdit(null, x => Name.PreferredName, "Preferred Name");
                canEdit(v, x => x.ValidFrom, "Date Of Birth");
                canSelectEnum(v, x => x.Gender, true);
                canEdit(v, x => x.Description);
            }
            else if (v?.IsOrganization() ?? false) {
                canSelect(v, x => x.OrganizationTypeId, organizationTypes);
                canEdit(null, x => Name.Name, "Name", true);
                canEdit(v, x => x.Description);
            }
            else canEdit(null, x => Name.Name, "Name", true);
        }
        protected override void dataInCreatePage() {
            if (isPerson()) {
                canEdit(null, x => Name.Name, "Family Name", true);
                canEdit(null, x => Name.GivenName, "First Name");
                canEdit(null, x => Name.MiddleName, "Middle Name");
                canEdit(null, x => Name.PreferredName, "Preferred Name");
                canEdit(null, x => x.ValidFrom, "Date Of Birth");
                canSelectEnum(null, x => x.Gender, true, IsoGender.NotKnown);
                canEdit(null, x => x.Description);
            } else if (isOrganization()) {
                canSelect(null, x => x.OrganizationTypeId, organizationTypes);
                canEdit(null, x => Name.Name, "Name", true);
                canEdit(null, x => x.Description);
            }
            else canEdit(null, x => Name.Name, "Name", true);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
            hasHidden(null, x => Name.Id, true);
            hasHidden(null, x => Name.PartyId);
            hasHidden(null, x => Name.PartyType, true);
            hasHidden(null, x => Name.NameType, true, false, "Name Type");
            if (isPerson()) hasHidden(null, x => x.Name, false, false, "Primary Name");
            else hasHidden(null, x => x.Name);
            hasHidden(null, x => x.PartyType, true);

        }
        protected override void validateItems(PartyData d1, PartyData d2) {
            if (isPerson()) allPropertiesAreEqual(d1, d2, nameof(d1.OrganizationTypeId), nameof(d1.Name), nameof(d1.Code), nameof(d1.Details), nameof(d1.ValidTo));
            else if (isOrganization()) allPropertiesAreEqual(d1, d2, nameof(d1.Gender), nameof(d1.Name), nameof(d1.Code), nameof(d1.Details), nameof(d1.ValidFrom), nameof(d1.ValidTo));
            else allPropertiesAreEqual(d1, d2, nameof(d1.OrganizationTypeId), nameof(d1.Gender), nameof(d1.Name), nameof(d1.Code), nameof(d1.Details), nameof(d1.ValidTo), nameof(d1.ValidFrom));
        }
        protected override HttpRequestMessage ComposeRequestMessage(IHtmlFormElement form, IHtmlInputElement button,
            PartyData d) => HtmlDoc.ComposeRequestMessage(form, button, toView(d), Name, nameof(Name));
        protected override PartyData correctOriginal(PartyData oldItem, PartyData newItem) {
            oldItem.PartyType = newItem.PartyType;
            update<IPartiesRepo, IParty>(PartyFactory.Create(oldItem));
            return oldItem;
        }
        protected override void changeValuesExceptId(PartyData d) {
            base.changeValuesExceptId(d);
            d.PartyType = partyType ?? d.PartyType;
        }
        protected override string baseClassName() => base.baseClassName().ReplaceFirst("ie", string.Empty);
        protected override PartyData randomItem() {
            var d = base.randomItem();
            d.PartyType = partyType ?? d.PartyType;
            return d;
        }
        protected override string pageUrl() {
            var url = base.pageUrl();
            if (partyType is null) return url;
            var typeIdx = Convert.ToInt32(partyType);
            url += $"&switchOfCreate={typeIdx}";
            return url;
        }
        [DataTestMethod]
        [DataRow(PartyType.Unspecified)]
        [DataRow(PartyType.Person)]
        [DataRow(PartyType.Organization)]
        public void CanDisplayDataTest(PartyType t) {
            partyType = t;
            CanDisplayDataTest();
        }
    }
    [TestClass] public class CreatePageTests :PartiesTests {
        [DataTestMethod]
        [DataRow(PartyType.Unspecified)]
        [DataRow(PartyType.Person)]
        [DataRow(PartyType.Organization)]
        public void CanClickCreateButtonTest(PartyType t) {
            partyType = t;
            canClickCreateButtonTest();
        }
    }
    [TestClass] public class DeletePageTests :PartiesTests {
        [DataTestMethod]
        [DataRow(PartyType.Unspecified)]
        [DataRow(PartyType.Person)]
        [DataRow(PartyType.Organization)]
        public void CanClickDeleteButtonTest(PartyType t) {
            partyType = t;
            canClickDeleteButtonTest();
        }
    }
    [TestClass] public class DetailsPageTests :PartiesTests {
        [DataTestMethod]
        [DataRow(PartyType.Unspecified)]
        [DataRow(PartyType.Person)]
        [DataRow(PartyType.Organization)]
        public void CanClickEditButtonInDetailsTest(PartyType t) {
            partyType = t;
            canClickEditButtonInDetailsTest();
        }
    }
    [TestClass] public class EditPageTests :PartiesTests {
        [DataTestMethod]
        [DataRow(PartyType.Unspecified)]
        [DataRow(PartyType.Person)]
        [DataRow(PartyType.Organization)]
        public void CanClickEditButtonTest(PartyType t) {
            partyType = t;
            canClickEditButtonTest();
        }
    }
    [TestClass] public class IndexPageTests :PartiesTests { }
}
