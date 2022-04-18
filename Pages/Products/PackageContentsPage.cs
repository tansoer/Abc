using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Products {
    public sealed class PackageContentsPage :ViewFactoryPage<PackageContentsPage, IPackageContentsRepo,
        PackageContent, PackageContentView, PackageContentData, PackageContentViewFactory> {
        protected override string title => ProductTitles.PackageContents;
        protected internal override string subtitle => HasFixedFilter ? $"{ProductName(FixedValue)}" : "";
        private readonly IProductsRepo productsRepo;
        public PackageContentsPage(IPackageContentsRepo r, IProductsRepo products) : base(r) {
            productsRepo = products;
        }
        private IEnumerable<SelectListItem> products;
        public IEnumerable<SelectListItem> Products => products ??= selectListByName<IProductsRepo, IProduct, ProductData>();
        private IEnumerable<SelectListItem> packages;
        public IEnumerable<SelectListItem> Packages => packages ??= selectListByName<IProductsRepo, IProduct, ProductData>(x => x.ProductKind == ProductKind.Package);
        private IEnumerable<SelectListItem> components;
        public IEnumerable<SelectListItem> Components => components ??= selectListByName<IPackageComponentsRepo, PackageComponent, PackageComponentData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
        };
        protected override void addFields() {
            addField(x => Item.PackageId, () => Packages, () => ProductName(Item.PackageId));
            addField(x => Item.ProductId, () => Products, () => ProductName(Item.ProductId));
            addField(x => Item.ComponentId, () => Components, () => ComponentName(Item.ComponentId));
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForViewers() {
            addFieldBefore(field(x => Item.Id), x => Item.PackageId);
            addFieldBefore(field(x => Item.Name), x => Item.PackageId);
            addFieldBefore(field(x => Item.Code), x => Item.ValidFrom);
            addFieldBefore(field(x => Item.Details), x => Item.ValidFrom);
        }
        protected override void fieldsForEditors() {
            fieldsForViewers();
            removeField(x => Item.Id);
        }
        protected internal override string pageUrl => ProductUrls.PackageContents;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new();
            if (HasFixedFilter) { 
                Item.PackageId = fixedValue;
                components = selectListByName<IPackageComponentsRepo, PackageComponent, PackageComponentData>(x => x.PackageTypeId == getPackageTypeId(fixedValue));
            }
        }
        internal string getPackageTypeId(string packageId) => productsRepo.Get(packageId)?.TypeId;
        public string ProductName(string id) => itemName(Products, id);
        public string ComponentName(string id) => itemName(Components, id);
    }
}
