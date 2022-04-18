using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Party.Pages.SignedEntityTypes {
    public abstract class SignedEntityTypesTests
        :BasePartyTests<SignedEntityTypeView, SignedEntityTypeData> {
        protected override string baseUrl() => PartyUrls.SignedEntityTypes;
        protected override SignedEntityTypeView toView(SignedEntityTypeData d)
            => new SignedEntityTypeViewFactory().Create(new SignedEntityType(d)); 
        protected override string performTitleCorrection(string title) {
            var t = base.performTitleCorrection(title);
            return t.Replace("Party", string.Empty);
        }
        protected override IEnumerable<Expression<Func<SignedEntityTypeView, object>>> 
            indexPageColumns =>
            new Expression<Func<SignedEntityTypeView, object>>[] { 
                x => x.Name,
                x => x.Code,
                x => x.Details,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :SignedEntityTypesTests { }
    [TestClass] public class DeletePageTests :SignedEntityTypesTests { }
    [TestClass] public class DetailsPageTests :SignedEntityTypesTests { }
    [TestClass] public class EditPageTests :SignedEntityTypesTests { }
    [TestClass] public class IndexPageTests :SignedEntityTypesTests { }
}
