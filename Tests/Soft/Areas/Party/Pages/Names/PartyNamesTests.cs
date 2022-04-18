using Abc.Aids.Enums;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Party.Pages.Names {
    public abstract class PartyNamesTests
        :BasePartyTests<PartyNameView, PartyNameData> {
        private PartyType partyType;
        protected override string baseUrl() => PartyUrls.Names;
        protected override PartyNameView toView(PartyNameData d)
            => new PartyNameViewFactory().Create(PartyNameFactory.Create(d));
        protected override void addRelatedItems(PartyNameData d) => correctNameValue(d);
        private static void correctNameValue(PartyNameData d) {
            if (isPerson(d?.PartyType) || d is null) return;
            d.GivenName = null;
            d.MiddleName = null;
            d.PreferredName = null;
        }
        protected override void validateItems(PartyNameData d1, PartyNameData d2) =>
            allPropertiesAreEqual(d1, d2, 
                nameof(d1.Code), 
                nameof(d1.Details), 
                nameof(d1.ValidFrom),
                nameof(d1.ValidTo));
        protected override IEnumerable<Expression<Func<PartyNameView, object>>> indexPageColumns =>
            new Expression<Func<PartyNameView, object>>[] {
            x => x.PartyType,
            x => x.Name,
            x => x.NameType
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.PartyType);
            if (item.PartyType == PartyType.Person) {
                canView(item, x => x.Name);
                canView(item, x => x.GivenName);
                canView(item, x => x.MiddleName);
                canView(item, x => x.PreferredName);
            } else if (item.PartyType == PartyType.Organization) {
                canView(item, x => x.Name);
            }
            canView(item, x => x.NameType);
        }
        private static bool isPerson(PartyType? t) => t == PartyType.Person;
        private static bool isOrganization(PartyType? t) => t == PartyType.Organization;
        protected override void dataInEditPage() {
            if (isPerson(item?.PartyType)) {
                canEdit(item, x => x.Name, true);
                canEdit(item, x => x.GivenName);
                canEdit(item, x => x.MiddleName);
                canEdit(item, x => x.PreferredName);
            } else {
                canEdit(item, x => x.Name, true);
            }
            canSelectEnum(item, x => x.NameType, true);
        }
        protected override void dataInCreatePage() {
            if (isPerson(item?.PartyType)) {
                canEdit(null, x => x.Name, true);
                canEdit(null, x => x.GivenName);
                canEdit(null, x => x.MiddleName);
                canEdit(null, x => x.PreferredName);
            } else {
                canEdit(null, x => x.Name, true);
            }
            canSelectEnum(null, x => x.NameType, true, NameType.Undefined);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.PartyType, true);
            hasHidden(null, x => x.Id, true);
            hasHidden(null, x => x.PartyId);
        }
        [TestMethod] public override void CanClickCreateButtonTest() => canClickCreateButtonTest();
        [DataTestMethod]
        [DataRow(PartyType.Person)]
        [DataRow(PartyType.Organization)]
        public void CanClickCreateButtonTest(PartyType t) {
            partyType = t;
            canClickCreateButtonTest();
        }
        protected override PartyNameData randomItem() {
            var d = base.randomItem();
            d.PartyType = partyType;
            return d;
        }
    }
    [TestClass] public class CreatePageTests :PartyNamesTests { }
    [TestClass] public class DeletePageTests :PartyNamesTests { }
    [TestClass] public class DetailsPageTests :PartyNamesTests { }
    [TestClass] public class EditPageTests :PartyNamesTests { }
    [TestClass] public class IndexPageTests :PartyNamesTests { }
}
