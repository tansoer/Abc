using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Preferences;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Party.Pages.Preferences {
    public abstract class PreferencesTests :BasePartyTests<PreferenceView, PreferenceData> {
        protected List<SelectListItem> preferenceTypes => createSelectList(classifiersDb.Classifiers,
            d => d.ClassifierType == ClassifierType.PartyPreference);
        protected List<SelectListItem> parties => createSelectList(db.Parties);
        protected List<SelectListItem> units => createSelectList(quantityDb.Units);
        protected List<SelectListItem> options => createSelectList(db.PreferenceOptions);
        protected override PreferenceView toView(PreferenceData d)
            => new PreferenceViewFactory().Create(new Preference(d));
        protected override string baseUrl() => PartyUrls.Preferences;
        protected override void addRelatedItems(PreferenceData d) {
            if (d is null) return;
            addClassifier(d.PreferenceTypeId, ClassifierType.PartyPreference);
            addParty(d.PartyId, PartyType.Person);
            addUnit(d.UnitId);
            addOption(d.PreferenceOptionId);
        }
        protected override string performTitleCorrection(string title) {
            var t = base.performTitleCorrection(title);
            return t.Replace("Party", string.Empty);
        }
        protected override IEnumerable<Expression<Func<PreferenceView, object>>> indexPageColumns =>
            new Expression<Func<PreferenceView, object>>[] {
                x => x.PartyId,
                x => x.PreferenceTypeId,
                x => x.Weight,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.PreferenceTypeId, Unspecified.String);
            canView(item, x => x.UnitId, Unspecified.String);
            canView(item, x => x.PreferenceOptionId, Unspecified.String);
            canView(item, x => x.PartyRoleId);
            canView(item, x => x.Weight);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canSelect(item, x => x.PartyId, parties);
            canSelect(item, x => x.PreferenceTypeId, preferenceTypes);
            canSelect(item, x => x.UnitId, units);
            canSelect(item, x => x.PreferenceOptionId, options);
            canEdit(item, x => x.PartyRoleId);
            canEdit(item, x => x.Weight, true, true);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canSelect(null, x => x.PartyId, parties);
            canSelect(null, x => x.PreferenceTypeId, preferenceTypes);
            canSelect(null, x => x.UnitId, units);
            canSelect(null, x => x.PreferenceOptionId, options);
            canEdit(null, x => x.PartyRoleId);
            canEdit(null, x => x.Weight, true, true, 0);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
            hasHidden(null, x => x.Name, true);
        }
        protected override void validateItems(PreferenceData d1, PreferenceData d2) {
            allPropertiesAreEqual(d1, d2,
                nameof(d1.Code),
                nameof(d1.Details));
        }
        protected override PreferenceData randomItem() => correctProperties(base.randomItem());
        private static PreferenceData correctProperties(PreferenceData d) {
            d.Weight = GetRandom.Double(-1000, 1000);
            return d;
        }
    }
    [TestClass] public class CreatePageTests :PreferencesTests { }
    [TestClass] public class DeletePageTests :PreferencesTests { }
    [TestClass] public class DetailsPageTests :PreferencesTests { }
    [TestClass] public class EditPageTests :PreferencesTests { }
    [TestClass] public class IndexPageTests :PreferencesTests { }
}
