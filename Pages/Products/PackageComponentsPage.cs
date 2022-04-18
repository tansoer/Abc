using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Products {
    public sealed class PackageComponentsPage :ViewFactoryPage<PackageComponentsPage, IPackageComponentsRepo, 
        PackageComponent, PackageComponentView, PackageComponentData, PackageComponentViewFactory> {
        protected override string title => ProductTitles.PackageComponents;
        protected internal override string subtitle => HasFixedFilter ? $"{ProductTypeName(FixedValue)}" : "";
        public PackageComponentsPage(IPackageComponentsRepo r) : base(r) { }
        private IEnumerable<SelectListItem> productTypes;
        public IEnumerable<SelectListItem> ProductTypes => productTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>();
        private IEnumerable<SelectListItem> packageTypes;
        public IEnumerable<SelectListItem> PackageTypes => packageTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>(x => x.ProductKind == ProductKind.Package);
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
        };
        protected override void addFields() {
            addField(x => Item.Name);
            addField(x => Item.PackageTypeId, () => PackageTypes, () => ProductTypeName(Item.PackageTypeId));
            addField(x => Item.ProductTypeId, () => ProductTypes, () => ProductTypeName(Item.ProductTypeId));
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForViewers() {
            addFieldBefore(field(x => Item.Id), x => Item.Name);
            addFieldBefore(field(x => Item.Code), x => Item.ValidFrom);
            addFieldBefore(field(x => Item.Details), x => Item.ValidFrom);
        }
        protected override void fieldsForEditors() {
            fieldsForViewers();
            removeField(x => Item.Id);
        }
        protected internal override string pageUrl => ProductUrls.PackageComponents;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new();
            if (HasFixedFilter) Item.PackageTypeId = fixedValue;
        }
        public string ProductTypeName(string id) => itemName(ProductTypes, id);
    }
}
