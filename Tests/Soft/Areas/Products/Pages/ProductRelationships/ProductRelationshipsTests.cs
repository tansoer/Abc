using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.ProductRelationships {
    public abstract class ProductRelationshipsTests :BaseProductTests<ProductRelationshipView, ProductRelationshipData> {
        protected List<SelectListItem> productRelationshipTypes => createSelectList(db.ProductRelationshipTypes);
        protected List<SelectListItem> products => createSelectList(db.Products);
        protected override string baseUrl() => ProductUrls.ProductRelationships;
        protected override ProductRelationshipView toView(ProductRelationshipData d)
            => new ProductRelationshipViewFactory().Create(new Abc.Domain.Products.ProductRelationship(d));
        protected override void changeValuesExceptId(ProductRelationshipData d) {
            var id = d.Id;
            d = random<ProductRelationshipData>();
            d.Id = id;
        }
        protected override string getItemId(ProductRelationshipData d) => d.Id;
        protected override void setItemId(ProductRelationshipData d, string id) => d.Id = id;
        protected override void addRelatedItems(ProductRelationshipData d) {
            if (d is null) return;
            addProductRelationshipType(d.RelationshipTypeId);
            addProduct(d.ProviderEntityId);
            addProduct(d.ConsumerEntityId);
        }
        protected override IEnumerable<Expression<Func<ProductRelationshipView, object>>> indexPageColumns =>
            new Expression<Func<ProductRelationshipView, object>>[] {
                x => x.RelationshipTypeId,
                x => x.ConsumerEntityId,
                x => x.ProviderEntityId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.RelationshipTypeId, Unspecified.String);
            canView(item, m => m.ConsumerEntityId, Unspecified.String);
            canView(item, m => m.ProviderEntityId, Unspecified.String);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canSelect(item, m => m.RelationshipTypeId, productRelationshipTypes);
            canSelect(item, m => m.ConsumerEntityId, products);
            canSelect(item, m => m.ProviderEntityId, products);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canSelect(null, x => x.RelationshipTypeId, productRelationshipTypes);
            canSelect(null, x => x.ConsumerEntityId, products);
            canSelect(null, x => x.ProviderEntityId, products);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :ProductRelationshipsTests { }
    [TestClass] public class DeletePageTests :ProductRelationshipsTests { }
    [TestClass] public class DetailsPageTests :ProductRelationshipsTests { }
    [TestClass] public class EditPageTests :ProductRelationshipsTests { }
    [TestClass] public class IndexPageTests :ProductRelationshipsTests { }
}
