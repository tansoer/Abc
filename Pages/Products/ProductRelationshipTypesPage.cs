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
    public sealed class ProductRelationshipTypesPage :ViewPage<
        ProductRelationshipTypesPage, IProductRelationshipTypesRepo, ProductRelationshipType, ProductRelationshipTypeView, ProductRelationshipTypeData> {
        protected override string title => ProductTitles.ProductRelationshipTypes;
        private IEnumerable<SelectListItem> baseTypes;
        private IEnumerable<SelectListItem> productTypes;
        public IEnumerable<SelectListItem> BaseTypes => baseTypes ??= selectListByName<IProductRelationshipTypesRepo, ProductRelationshipType, ProductRelationshipTypeData>();
        public IEnumerable<SelectListItem> ProductTypes => productTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>();
        public ProductRelationshipTypesPage(IProductRelationshipTypesRepo r) :base(r) {
        }
        protected internal override string pageUrl => ProductUrls.ProductRelationshipTypes;
        protected override void tableColumns() {
            tableColumn(x => Item.BaseTypeId);
            tableColumn(x => Item.ConsumerTypeId);
            tableColumn(x => Item.ProviderTypeId);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        public override IHtmlContent ValueFor(IHtmlHelper<ProductRelationshipTypesPage> html, int i) => i switch {
            0 => html.Raw(BaseTypeName(Item?.BaseTypeId)),
            1 => html.Raw(ProductTypeName(Item?.ConsumerTypeId)),
            2 => html.Raw(ProductTypeName(Item?.ProviderTypeId)),
            _ => base.ValueFor(html, i)
        };
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.BaseTypeId, () => BaseTypeName(Item.BaseTypeId));
            valueViewer(x => Item.ProviderTypeId, () => ProductTypeName(Item.ProviderTypeId));
            valueViewer(x => Item.ConsumerTypeId, () => ProductTypeName(Item.ConsumerTypeId));
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.BaseTypeId, () => BaseTypes);
            valueEditor(x => Item.ProviderTypeId, () => ProductTypes);
            valueEditor(x => Item.ConsumerTypeId, () => ProductTypes);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override ProductRelationshipType toObject(ProductRelationshipTypeView v) =>
            new ProductRelationshipTypeViewFactory().Create(v);
        protected internal override ProductRelationshipTypeView toView(ProductRelationshipType o) =>
            new ProductRelationshipTypeViewFactory().Create(o);
        public override IActionResult OnGetCreate(
            string sortOrder, string searchString, int? pageIndex,
            string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new();
            return base.OnGetCreate(sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue, switchOfCreate);
        }
        public string BaseTypeName(string id) => itemName(BaseTypes, id);
        public string ProductTypeName(string id) => itemName(ProductTypes, id);
    }
}
