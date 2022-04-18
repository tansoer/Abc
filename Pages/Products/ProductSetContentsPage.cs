using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Products {
    public sealed class ProductSetContentsPage :ViewFactoryPage<ProductSetContentsPage, IProductSetContentsRepo, 
        ProductSetContent, ProductSetContentView, ProductSetContentData, ProductSetContentViewFactory> {
        protected override string title => ProductTitles.ProductSetContents;
        protected internal override string subtitle => HasFixedFilter ? $"{ProductSetName(FixedValue)}" : "";
        public ProductSetContentsPage(IProductSetContentsRepo r) :base(r) {}
        private IEnumerable<SelectListItem> productSets;
        public IEnumerable<SelectListItem> ProductSets => productSets ??= selectListByName<IProductSetsRepo, ProductSet, ProductSetData>();
        private IEnumerable<SelectListItem> productTypes;
        public IEnumerable<SelectListItem> ProductTypes => productTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
        };
        protected override void addFields() {
            addField(x => Item.Name);
            addField(x => Item.ProductSetId, () => ProductSets, () => ProductSetName(Item.ProductSetId));
            addField(x => Item.ProductTypeId, () => ProductTypes, () => ProductTypeName(Item.ProductTypeId));
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForViewers() {
            addFieldBefore(field(x => Item.Id), x => Item.Name);
            addFieldAfter(field(x => Item.Code), x => Item.Name);
            addFieldBefore(field(x => Item.Details), x => Item.ValidFrom);
        }
        protected override void fieldsForEditors() {
            fieldsForViewers();
            removeField(x => Item.Id);
        }
        protected internal override string pageUrl => ProductUrls.ProductSetContents;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new();
            if (HasFixedFilter) Item.ProductSetId = fixedValue;
        }
        public string ProductSetName(string id) => itemName(ProductSets, id);
        public string ProductTypeName(string id) => itemName(ProductTypes, id);
    }
}
