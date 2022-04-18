using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Currencies.Pages.RateTypes {
    public abstract class RateTypesTests :BaseCurrenciesTests<RateTypeView, RateTypeData> {
        protected override string baseUrl() => MoneyUrls.RateTypes;
        protected override void changeValuesExceptId(RateTypeData d) {
            var id = d.Id;
            d = random<RateTypeData>();
            d.Id = id;
        }
        protected override string getItemId(RateTypeData d) => d.Id;
        protected override void setItemId(RateTypeData d, string id) => d.Id = id;
        protected override RateTypeView toView(RateTypeData d) 
            => new RateTypeViewFactory().Create(new RateType(d));
        protected override IEnumerable<Expression<Func<RateTypeView, object>>> indexPageColumns =>
            new Expression<Func<RateTypeView, object>>[] {
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
    [TestClass] public class CreatePageTests :RateTypesTests { }
    [TestClass] public class DeletePageTests :RateTypesTests { }
    [TestClass] public class DetailsPageTests :RateTypesTests { }
    [TestClass] public class EditPageTests :RateTypesTests { }
    [TestClass] public class IndexPageTests :RateTypesTests { }
}
