using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Products {
    public sealed class ProductRelationshipsPage :ViewPage<
        ProductRelationshipsPage, IProductRelationshipsRepo, ProductRelationship, ProductRelationshipView, ProductRelationshipData> {
        protected override string title => ProductTitles.ProductRelationships;
        private IEnumerable<SelectListItem> productRelationshipTypes;
        private IEnumerable<SelectListItem> products;
        public IEnumerable<SelectListItem> ProductRelationshipTypes=> productRelationshipTypes 
            ??= selectListByName<IProductRelationshipTypesRepo, ProductRelationshipType, ProductRelationshipTypeData>();
        public IEnumerable<SelectListItem> Products => products 
            ??= selectListByName<IProductsRepo, IProduct, ProductData>();
        public ProductRelationshipsPage(IProductRelationshipsRepo r) :base(r) {
        }
        protected internal override string pageUrl => ProductUrls.ProductRelationships;
        protected override void tableColumns() {
            tableColumn(x => Item.RelationshipTypeId);
            tableColumn(x => Item.ConsumerEntityId);
            tableColumn(x => Item.ProviderEntityId);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        public override IHtmlContent ValueFor(IHtmlHelper<ProductRelationshipsPage> html, int i) => i switch {
            0 => html.Raw(ProductRelationshipTypeName(Item?.RelationshipTypeId)),
            1 => html.Raw(ProductName(Item?.ConsumerEntityId)),
            2 => html.Raw(ProductName(Item?.ProviderEntityId)),
            _ => base.ValueFor(html, i)
        };
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.RelationshipTypeId, () => ProductRelationshipTypeName(Item.RelationshipTypeId));
            valueViewer(x => Item.ConsumerEntityId, () => ProductName(Item.ConsumerEntityId));
            valueViewer(x => Item.ProviderEntityId, () => ProductName(Item.ProviderEntityId));
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.RelationshipTypeId, () => ProductRelationshipTypes);
            valueEditor(x => Item.ConsumerEntityId, () => Products);
            valueEditor(x => Item.ProviderEntityId, () => Products);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override ProductRelationship toObject(ProductRelationshipView v) 
            => new ProductRelationshipViewFactory().Create(v);
        protected internal override ProductRelationshipView toView(ProductRelationship o) 
            => new ProductRelationshipViewFactory().Create(o);
        public override IActionResult OnGetCreate(
            string sortOrder, string searchString, int? pageIndex,
            string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new ();
            return base.OnGetCreate(sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue, switchOfCreate);
        }
        public string ProductRelationshipTypeName(string id) => itemName(ProductRelationshipTypes, id);
        public string ProductName(string id) => itemName(Products, id);
    }
}
