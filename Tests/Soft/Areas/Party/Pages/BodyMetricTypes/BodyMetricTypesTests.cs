using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Party.Pages.BodyMetricTypes {
    public abstract class BodyMetricTypesTests : BasePartyTests<BodyMetricTypeView, BodyMetricTypeData> {
        protected List<SelectListItem> baseTypes { get => db.BodyMetricTypes.Select(x => new SelectListItem(x.Name, x.Id)).ToList();}
        protected List<SelectListItem> ruleSets = new ();
        protected override string baseUrl() => PartyUrls.BodyMetricTypes;
        protected override IEnumerable<Expression<Func<BodyMetricTypeView, object>>> indexPageColumns =>
            new Expression<Func<BodyMetricTypeView, object>>[] {
                x => x.BaseTypeId,
                x => x.RuleSetId,
                x => x.Name,
                x => x.Code,
                x => x.Details,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override BodyMetricTypeView toView(BodyMetricTypeData d)
            => new BodyMetricTypeViewFactory().Create(new Abc.Domain.Parties.Attributes.BodyMetricType(d));
        protected override void dataInDetailsPage() {
            canView(item, x => x.BaseTypeId, Unspecified.String);
            canView(item, x => x.RuleSetId, Unspecified.String);
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canSelect(item, x => x.BaseTypeId, baseTypes, false, true);
            //canSelect(item, x => x.RuleSetId, ruleSets, false, true); NEEDS ACCESS TO db.RuleSets !!!
            canEdit(item, x => x.Name, true);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canSelect(null, x => x.BaseTypeId, baseTypes);
            //canSelect(null, x => x.RuleSetId, ruleSets); NEEDS ACCESS TO db.RuleSets !!!
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() 
            => hasHidden(item, x => x.Id, true);
        protected override BodyMetricTypeData randomItem() {
            var i = base.randomItem();
            i.BaseTypeId = Unspecified.String;
            i.RuleSetId = Unspecified.String;
            return i;
        }
    }
    [TestClass] public class CreatePageTests :BodyMetricTypesTests { }
    [TestClass] public class DeletePageTests :BodyMetricTypesTests { }
    [TestClass] public class DetailsPageTests :BodyMetricTypesTests { }
    [TestClass] public class EditPageTests :BodyMetricTypesTests { }
    [TestClass] public class IndexPageTests :BodyMetricTypesTests { }
}