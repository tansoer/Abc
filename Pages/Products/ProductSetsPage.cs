using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abc.Pages.Products {
    public sealed class ProductSetsPage :ViewFactoryPage<ProductSetsPage, IProductSetsRepo,
        ProductSet, ProductSetView, ProductSetData, ProductSetViewFactory> {
        protected override string title => ProductTitles.ProductSets;
        protected internal override string subtitle => HasFixedFilter ? $"{PackageTypeName(FixedValue)}" : "";
        public ProductSetsPage(IProductSetsRepo r) : base(r) { } 
        private IEnumerable<SelectListItem> packageTypes;
        public IEnumerable<SelectListItem> PackageTypes => packageTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>( x => x.ProductKind == ProductKind.Package);
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
        };
        protected override void addFields() {
            addField(x => Item.Name);
            addField(x => Item.PackageTypeId, () => PackageTypes, () => PackageTypeName(Item.PackageTypeId));
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
        protected internal override string pageUrl => ProductUrls.ProductSets;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new();
            if (HasFixedFilter) Item.PackageTypeId = fixedValue;
        }
        public string PackageTypeName(string id) => itemName(PackageTypes, id);
        public async Task<IActionResult> OnGetShowProductSetContentsAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<ProductSetContentData>(x => x.ProductSetId);
            var page = ProductUrls.ProductSetContents;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder=", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
    }
}
