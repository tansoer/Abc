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

namespace Abc.Tests.Soft.Areas.Party.Pages.Suffixes {
    public abstract class NameSuffixesTests : BasePartyTests<PersonNameSuffixView, NameSuffixData> {
        protected List<SelectListItem> parties
            => db.PartyNames.Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected List<SelectListItem> suffixes
            => classifiersDb.Classifiers
                .Where(x => x.ClassifierType == ClassifierType.PersonNameSuffix)
                .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected override string baseUrl() => PartyUrls.PersonNameSuffixes;
        protected override PersonNameSuffixView toView(NameSuffixData d)
            => new PersonNameSuffixViewFactory().Create(new NameSuffix(d));
        protected override void addRelatedItems(NameSuffixData d) {
            if (d is null) return;
            addPartyNameForPartyId(d.PartyId);
            addClassifier(d.SuffixTypeId, ClassifierType.PersonNameSuffix);
        }
        protected override IEnumerable<Expression<Func<PersonNameSuffixView, object>>>
            indexPageColumns =>
            new Expression<Func<PersonNameSuffixView, object>>[] {
            x => x.NameId,
            x => x.SuffixTypeId,
            x => x.Index,
            x => x.Details,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.NameId, selectedItemName(parties, item.NameId));
            canView(item, x => x.SuffixTypeId, selectedItemName(suffixes, item.SuffixTypeId));
            canView(item, m => m.Index);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canSelect(item, x => x.NameId, parties, true);
            canSelect(item, x => x.SuffixTypeId, suffixes);
            canEdit(item, m => m.Index, true);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canSelect(null, x => x.NameId, parties, true);
            canSelect(null, x => x.SuffixTypeId, suffixes);
            canEdit(null, m => m.Index, true, false, suffixes.Count +1);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
            hasHidden(null, x => x.Name, true);
        }
        protected override void validateItems(NameSuffixData d1, NameSuffixData d2)
            => allPropertiesAreEqual(d1, d2, nameof(d1.Code), nameof(d1.PartyId), nameof(d1.NameId), nameof(d1.SuffixTypeId));
        protected override string baseClassName() => "NameSuffix";
        protected override string performTitleCorrection(string title) => "NameSuffix";
    }
    [TestClass] public class CreatePageTests :NameSuffixesTests { }
    [TestClass] public class DeletePageTests :NameSuffixesTests { }
    [TestClass] public class DetailsPageTests :NameSuffixesTests { }
    [TestClass] public class EditPageTests :NameSuffixesTests { }
    [TestClass] public class IndexPageTests :NameSuffixesTests { }
}
