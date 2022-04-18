using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Party.Pages.Prefixes {
    public abstract class NamePrefixesTests : BasePartyTests<PersonNamePrefixView, NamePrefixData> {
        protected List<SelectListItem> parties 
            => db.PartyNames.Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected List<SelectListItem> prefixes
            => classifiersDb.Classifiers
                .Where(x => x.ClassifierType == ClassifierType.PersonNamePrefix)
                .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected override string baseClassName() => "NamePrefix";
        protected override PersonNamePrefixView toView(NamePrefixData d)
            => new PersonNamePrefixViewFactory().Create(new NamePrefix(d));
        protected override string performTitleCorrection(string title) => "NamePrefix";
        protected override string baseUrl() => PartyUrls.PersonNamePrefixes;
        protected override void addRelatedItems(NamePrefixData d) {
            if (d is null) return;
            addPartyNameForPartyId(d.PartyId);
            addClassifier(d.PrefixTypeId, ClassifierType.PersonNamePrefix);
        }
        protected override IEnumerable<Expression<Func<PersonNamePrefixView, object>>> 
            indexPageColumns =>
            new Expression<Func<PersonNamePrefixView, object>>[] {
            x => x.NameId,
            x => x.PrefixTypeId,
            x => x.Index,
            x => x.Details,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.NameId, selectedItemName(parties, item.NameId));
            canView(item, x => x.PrefixTypeId, selectedItemName(prefixes, item.PrefixTypeId));
            canView(item, x => x.Index);
            canView(item, x => x.Details);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canSelect(item, x => x.NameId, parties, true);
            canSelect(item, x => x.PrefixTypeId, prefixes);
            canEdit(item, x => x.Index, true);
            canEdit(item, x => x.Details);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canSelect(null, x => x.NameId, parties, true);
            canSelect(null, x => x.PrefixTypeId, prefixes);
            canEdit(null, x => x.Index, true, false, prefixes.Count + 1);
            canEdit(null, x => x.Details);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
            hasHidden(null, x => x.Name, true);
        }
        protected override void validateItems(NamePrefixData d1, NamePrefixData d2)
            => allPropertiesAreEqual(d1, d2, nameof(d1.Code), nameof(d1.PartyId), nameof(d1.NameId), nameof(d1.PrefixTypeId));
    }
    [TestClass] public class CreatePageTests :NamePrefixesTests { }
    [TestClass] public class DeletePageTests :NamePrefixesTests { }
    [TestClass] public class DetailsPageTests :NamePrefixesTests { }
    [TestClass] public class EditPageTests :NamePrefixesTests { }
    [TestClass] public class IndexPageTests :NamePrefixesTests { }
}
