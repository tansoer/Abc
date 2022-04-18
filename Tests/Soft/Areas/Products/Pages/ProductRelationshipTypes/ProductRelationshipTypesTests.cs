using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.ProductRelationshipTypes {
    public abstract class ProductRelationshipTypesTests :BaseProductTests<ProductRelationshipTypeView, ProductRelationshipTypeData> {
        protected List<SelectListItem> baseTypes => createSelectList(db.ProductRelationshipTypes);
        protected List<SelectListItem> productTypes => createSelectList(db.ProductTypes);
        protected override string baseUrl() => ProductUrls.ProductRelationshipTypes;
        protected override ProductRelationshipTypeView toView(ProductRelationshipTypeData d)
            => new ProductRelationshipTypeViewFactory().Create(new Abc.Domain.Products.ProductRelationshipType(d));
        protected override void changeValuesExceptId(ProductRelationshipTypeData d) {
            var id = d.Id;
            d = random<ProductRelationshipTypeData>();
            d.Id = id;
        }
        protected override string getItemId(ProductRelationshipTypeData d) => d.Id;
        protected override void setItemId(ProductRelationshipTypeData d, string id) => d.Id = id;
        protected override void addRelatedItems(ProductRelationshipTypeData d) {
            if (d is null) return;
            addProductType(d.ConsumerTypeId);
            addProductType(d.ProviderTypeId);
            addProductRelationshipType(d.BaseTypeId);
        }
        protected override IEnumerable<Expression<Func<ProductRelationshipTypeView, object>>> indexPageColumns =>
            new Expression<Func<ProductRelationshipTypeView, object>>[] {
                x => x.BaseTypeId,
                x => x.ConsumerTypeId,
                x => x.ProviderTypeId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.BaseTypeId, Unspecified.String);
            canView(item, m => m.ProviderTypeId, Unspecified.String);
            canView(item, m => m.ConsumerTypeId, Unspecified.String);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canSelect(item, m => m.BaseTypeId, baseTypes);
            canSelect(item, m => m.ProviderTypeId, productTypes);
            canSelect(item, m => m.ConsumerTypeId, productTypes);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canSelect(null, x => x.BaseTypeId, baseTypes);
            canSelect(null, x => x.ProviderTypeId, productTypes);
            canSelect(null, x => x.ConsumerTypeId, productTypes);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :ProductRelationshipTypesTests { }
    [TestClass] public class DeletePageTests :ProductRelationshipTypesTests { }
    [TestClass] public class DetailsPageTests :ProductRelationshipTypesTests { }
    [TestClass] public class EditPageTests :ProductRelationshipTypesTests { }
    [TestClass] public class IndexPageTests :ProductRelationshipTypesTests { }
}
