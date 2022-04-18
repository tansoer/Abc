using Abc.Aids.Enums;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Attributes;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using AngleSharp.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Party.Pages.PersonEthnicities {
    public abstract class PersonEthnicitiesTests
        :BasePartyTests<PersonEthnicityView, PersonEthnicityData> {
        protected List<SelectListItem> parties => createSelectList(db.PartyNames);
        protected List<SelectListItem> ethnicities => 
            createSelectList(classifiersDb.Classifiers, x => x.ClassifierType == ClassifierType.PartyEthnicity);
        protected override PersonEthnicityView toView(PersonEthnicityData d) 
            => new PersonEthnicityViewFactory().Create(new PersonEthnicity(d));
        protected override string baseUrl() => PartyUrls.PersonEthnicities;
        protected override string performTitleCorrection(string title) {
            var t = title[0..^3];
            return t.Replace("Party", string.Empty);
        }
        protected override void addRelatedItems(PersonEthnicityData d) {
            if (d is null) return;
            addParty(d.PartyId, PartyType.Person);
            addEthnicity(d.EthnicityId);
        }
        protected override IEnumerable<Expression<Func<PersonEthnicityView, object>>> indexPageColumns =>
            new Expression<Func<PersonEthnicityView, object>>[] {
            x => x.PartyId,
            x => x.EthnicityId,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.PartyId, Unspecified.String);
            canView(item, x => x.EthnicityId, Unspecified.String);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canSelect(item, x => x.PartyId, parties);
            canSelect(item, x => x.EthnicityId, ethnicities);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canSelect(null, x => x.PartyId, parties);
            canSelect(null, x => x.EthnicityId, ethnicities);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            hasHidden(item, x => x.PartyId);
            hasHidden(item, x => x.Name, true);
        }
        protected override void validateItems(PersonEthnicityData d1, PersonEthnicityData d2) =>
            allPropertiesAreEqual(d1, d2, 
                nameof(d1.Code),
                nameof(d1.Details));
        protected override string baseClassName() => base.baseClassName().ReplaceFirst("ie", string.Empty);
    }
    [TestClass] public class CreatePageTests :PersonEthnicitiesTests { }
    [TestClass] public class DeletePageTests :PersonEthnicitiesTests { }
    [TestClass] public class DetailsPageTests :PersonEthnicitiesTests { }
    [TestClass] public class EditPageTests :PersonEthnicitiesTests { }
    [TestClass] public class IndexPageTests :PersonEthnicitiesTests { }
}
