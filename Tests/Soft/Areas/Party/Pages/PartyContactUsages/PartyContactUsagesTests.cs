using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Party.Pages.PartyContactUsages {
    public abstract class PartyContactUsagesTests
        :BasePartyTests<PartyContactUsageView, PartyContactUsageData> {
        protected List<SelectListItem> parties
            => db.PartyNames.Select(c => new SelectListItem(c.ToString(), c.Id)).ToList();
        protected List<SelectListItem> usages
            => classifiersDb.Classifiers
                .Where(x => x.ClassifierType == ClassifierType.ContactUsage)
                .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected List<SelectListItem> contacts
            => db.PartyContacts
              .Select(c => new SelectListItem(c.ToString(), c.Id)).ToList();
        protected override string baseUrl() => PartyUrls.PartyContactUsages;
        protected override PartyContactUsageView toView(PartyContactUsageData d)
            => new PartyContactUsageViewFactory().Create(new PartyContactUsage(d));
        protected override void addRelatedItems(PartyContactUsageData d) {
            if (d is null) return;
            addPartyNameForPartyId(d.PartyId);
            addClassifier(d.Code, ClassifierType.ContactUsage);
            addContact(d.Name);
        }
        protected override IEnumerable<Expression<Func<PartyContactUsageView, object>>>
            indexPageColumns =>
            new Expression<Func<PartyContactUsageView, object>>[] {
            x => x.PartyId,
            x => x.Code,
            x => x.Name,
            x => x.Details,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.PartyId, selectedItemName(parties, item.PartyId));
            canView(item, x => x.Code, selectedItemName(usages, item.Code));
            canView(item, x => x.Name, selectedItemName(contacts, item.Name));
            canView(item, x => x.Details);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canSelect(item, x => x.PartyId, parties);
            canSelect(item, x => x.Code, usages);
            canSelect(item, x => x.Name, contacts);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canSelect(null, x => x.PartyId, parties);
            canSelect(null, x => x.Code, usages);
            canSelect(null, x => x.Name, contacts);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :PartyContactUsagesTests { }
    [TestClass] public class DeletePageTests :PartyContactUsagesTests { }
    [TestClass] public class DetailsPageTests :PartyContactUsagesTests { }
    [TestClass] public class EditPageTests :PartyContactUsagesTests { }
    [TestClass] public class IndexPageTests :PartyContactUsagesTests { }
}
