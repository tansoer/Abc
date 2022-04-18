using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products.Features;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.FeatureTypes {
    public abstract class FeatureTypesTests :BaseProductTests<FeatureTypeView, FeatureTypeData> {

        protected List<SelectListItem> productTypes => createSelectList(db.ProductTypes);

        protected override string baseUrl() => ProductUrls.FeatureTypes;
        protected override void changeValuesExceptId(FeatureTypeData d) {
            var id = d.Id;
            d = random<FeatureTypeData>();
            d.Id = id;
        }
        protected override string getItemId(FeatureTypeData d) => d.Id;
        protected override void setItemId(FeatureTypeData d, string id) => d.Id = id;
        protected override void addRelatedItems(FeatureTypeData d) {
            if (d is null) return;
            addProductType(d.ProductTypeId);
        }
        protected override FeatureTypeView toView(FeatureTypeData d) => new FeatureTypeViewFactory().Create(new FeatureType(d));
        protected override IEnumerable<Expression<Func<FeatureTypeView, object>>> indexPageColumns =>
            new Expression<Func<FeatureTypeView, object>>[] {
                x => x.IsMandatory,
                x => x.ProductTypeId,
                x => x.Name,
                x => x.Code,
                x => x.NumericCode,
                x => x.Details,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            FeatureTypeView v = toView(item);
            canView(v, m => m.Id);
            canViewCheckBox(v, m => m.IsMandatory);
            canViewCheckBox(v, m => m.MustSatisfyAll);
            canView(v, m => m.BaseTypeId);
            canView(v, m => m.ProductTypeId, Unspecified.String);
            canView(v, m => m.ValueType);
            canView(v, m => m.Name);
            canView(v, m => m.Code);
            canView(v, m => m.NumericCode);
            canView(v, m => m.Details);
            canView(v, m => m.ValidFrom);
            canView(v, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            FeatureTypeView v = toView(item);
            canClickCheckBox(v, m => m.IsMandatory, true, true);
            canClickCheckBox(v, m => m.MustSatisfyAll, true, true);
            canEdit(v, m => m.BaseTypeId);
            canSelect(v, m => m.ProductTypeId, productTypes);
            canSelectEnum(v, m => m.ValueType, true, Abc.Data.Common.ValueType.Unspecified);
            canEdit(v, m => m.Name, true);
            canEdit(v, m => m.Code);
            canEdit(v, m => m.NumericCode, true, false, 0);
            canEdit(v, m => m.Details);
            canEdit(v, m => m.ValidFrom);
            canEdit(v, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canClickCheckBox(null, m => m.IsMandatory, true, true);
            canClickCheckBox(null, m => m.MustSatisfyAll, true, true);
            canEdit(null, m => m.BaseTypeId);
            canSelect(null, m => m.ProductTypeId, productTypes);
            canSelectEnum(null, m => m.ValueType, true, Abc.Data.Common.ValueType.Unspecified);
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.NumericCode, true, false, 0);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :FeatureTypesTests { }
    [TestClass] public class DeletePageTests :FeatureTypesTests { }
    [TestClass] public class DetailsPageTests :FeatureTypesTests {
        public void CanClicNavigationLinks() {
            clickNavigationLink("showFeatures", ProductUrls.Features);
            clickNavigationLink("showPossibleValues", ProductUrls.PossibleFeatureValues);
        }
    }
    [TestClass] public class EditPageTests :FeatureTypesTests { }
    [TestClass] public class IndexPageTests :FeatureTypesTests { }
}
