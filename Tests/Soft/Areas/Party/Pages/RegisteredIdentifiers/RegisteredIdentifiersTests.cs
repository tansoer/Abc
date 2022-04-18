using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Parties.Identifiers;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Party.Pages.RegisteredIdentifiers {
    public abstract class RegisteredIdentifiersTests
        :BasePartyTests<RegisteredIdentifierView, RegisteredIdentifierData> {
        protected List<SelectListItem> parties 
            => db.PartyNames.Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected List<SelectListItem> identifiers
            => classifiersDb.Classifiers
                .Where(x => x.ClassifierType == ClassifierType.RegisteredIdentifier)
                .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected List<SelectListItem> authorities 
            => classifiersDb.Classifiers
              .Where(x => x.ClassifierType == ClassifierType.RegistrationAuthority)
              .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected override string baseUrl() => PartyUrls.RegisteredIdentifiers;
        protected override string performTitleCorrection(string title) {
            var t = base.performTitleCorrection(title);
            return t.Replace("Party", string.Empty);
        }
        protected override RegisteredIdentifierView toView(RegisteredIdentifierData d)
            => new RegisteredIdentifierViewFactory().Create(new RegisteredIdentifier(d));
        protected override void addRelatedItems(RegisteredIdentifierData d) {
            if (d is null) return;
            d.Code = null;
            addPartyNameForPartyId(d.PartyId);
            addClassifier(d.RegisteredIdentifierTypeId, ClassifierType.RegisteredIdentifier);
            addRegistrationAuthority(d.RegistrationAuthorityId);
        }
        protected override IEnumerable<Expression<Func<RegisteredIdentifierView, object>>> 
            indexPageColumns =>
            new Expression<Func<RegisteredIdentifierView, object>>[] {
            x => x.PartyId,
            x => x.RegisteredIdentifierTypeId,
            x => x.Name,
            x => x.RegistrationAuthorityId,
            x => x.Details,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.PartyId, selectedItemName(parties, item.PartyId));
            canView(item, x => x.RegisteredIdentifierTypeId, selectedItemName(identifiers, item.RegisteredIdentifierTypeId));
            canView(item, x => x.Name);
            canView(item, x => x.RegistrationAuthorityId, selectedItemName(authorities, item.RegistrationAuthorityId));
            canView(item, x => x.Details);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canSelect(item, x => x.PartyId, parties);
            canSelect(item, x => x.RegisteredIdentifierTypeId, identifiers);
            canEdit(item, m => m.Name, true);
            canSelect(item, x => x.RegistrationAuthorityId, authorities);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canSelect(null, x => x.PartyId, parties);
            canSelect(null, x => x.RegisteredIdentifierTypeId, identifiers);
            canEdit(null, m => m.Name, true);
            canSelect(null, x => x.RegistrationAuthorityId, authorities);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :RegisteredIdentifiersTests { }
    [TestClass] public class DeletePageTests :RegisteredIdentifiersTests { }
    [TestClass] public class DetailsPageTests :RegisteredIdentifiersTests { }
    [TestClass] public class EditPageTests :RegisteredIdentifiersTests { }
    [TestClass] public class IndexPageTests :RegisteredIdentifiersTests { }
}
