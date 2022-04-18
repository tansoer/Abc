using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Currencies.Pages.Cards {
    public abstract class CardsTests :BaseCurrenciesTests<CardAssociationView, CardAssociationData> {
        protected override string baseUrl() => MoneyUrls.Cards;
        protected override void changeValuesExceptId(CardAssociationData d) {
            var id = d.Id;
            d = random<CardAssociationData>();
            d.Id = id;
        }
        protected override CardAssociationView toView(CardAssociationData d) 
            => new CardAssociationViewFactory().Create(new CardAssociation(d));
        protected override string getItemId(CardAssociationData d) => d.Id;
        protected override void setItemId(CardAssociationData d, string id) => d.Id = id;
        protected override void isCorrectPageName() {
            var n = baseClassName().ToLower();
            var title = pageTitle().ToLower();
            isTrue(title.Contains(n));
        }
        protected override IEnumerable<Expression<Func<CardAssociationView, object>>> indexPageColumns =>
            new Expression<Func<CardAssociationView, object>>[] {
                x => x.Id,
                x => x.Name,
                x => x.Code,
                x => x.Details,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Id, true);
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Id, true);
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
    }
    [TestClass] public class CreatePageTests :CardsTests { }
    [TestClass] public class DeletePageTests :CardsTests { }
    [TestClass] public class DetailsPageTests :CardsTests { }
    [TestClass] public class EditPageTests :CardsTests { }
    [TestClass] public class IndexPageTests :CardsTests { }
}
