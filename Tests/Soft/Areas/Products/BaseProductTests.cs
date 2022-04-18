using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Domain.Parties.Signatures;
using Abc.Facade.Common;
using Abc.Infra.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Products {

    public abstract class BaseProductTests<TView, TData> :BaseAcceptanceTests<TView, TData, ProductDb>
        where TData : EntityBaseData where TView : EntityBaseView {
        protected override void doOnCreated(ProductDb c) => clearAll(c);
        [TestCleanup] public override void TestCleanup() {
            base.TestCleanup();
            clearAll(db);
        }
        private void clearAll(ProductDb c) {
            clear(c.Products);
            clear(c.ProductTypes);
            clear(c.Features);
            clear(c.FeatureTypes);
            clear(c.PossibleFeatureValues);
            clear(c.Batches);
            clear(c.BatchCheckedByParties);
            clear(c.CatalogedProducts);
            clear(c.CatalogEntries);
            clear(c.ProductCategories);
            clear(c.ProductCatalogs);
            clear(c.PackageComponents);
            clear(c.PackageContents);
            clear(c.ProductSets);
            clear(c.ProductSetContents);
            clear(c.ProductInclusions);
            clear(c.ProductRelationshipTypes);
            clear(c.ProductRelationships);
            clear(c.Prices);
            clear(c.PriceEndorsements);
            clear(c.ContainerItems);
        }
        protected void addProduct(string id) => addRandomRecord<ProductData>(id);
        protected void addProduct(string id, ProductKind kind) => addRandomRecord<ProductData>(d => {
            d.Id = id;
            d.ProductKind = kind;
        });
        protected void addProductType(string id) => addRandomRecord<ProductTypeData>(id);
        protected void addProductType(string id, ProductKind kind, bool isBaseType = false) => addRandomRecord<ProductTypeData>(d => {
            d.Id = id;
            d.ProductKind = kind;
            d.IsBaseType = isBaseType;
        });
        protected void addFeature(string id) => addRandomRecord<FeatureData>(id);
        protected void addFeatureType(string id) => addRandomRecord<FeatureTypeData>(id);
        protected void addPossibleFeatureValue(string id) => addRandomRecord<PossibleFeatureValueData>(id);
        protected void addBatch(string id) => addRandomRecord<BatchData>(id);
        protected void addBatchCheckedByParty(string batchId, string partySignatureId)
            => addRandomRecord<BatchCheckedByData>(d => {
                d.BatchId = batchId;
                d.PartySignatureId = partySignatureId;
            });
        protected void addCatalogedProduct(string catalogEntryId, string productTypeId) => addRandomRecord<CatalogedProductData>(d => {
            d.CatalogEntryId = catalogEntryId;
            d.ProductTypeId = productTypeId;
        });
        protected void addCatalogEntry(string id) => addRandomRecord<CatalogEntryData>(id);
        protected void addProductCategory(string id) => addRandomRecord<ProductCategoryData>(id);
        protected void addProductCatalog(string id) => addRandomRecord<ProductCatalogData>(id);
        protected void addPackageComponent(string id) => addRandomRecord<PackageComponentData>(id);
        protected void addPackageComponent(string packageTypeId, string productTypeId) => addRandomRecord<PackageComponentData>(d => {
            d.PackageTypeId = packageTypeId;
            d.ProductTypeId = productTypeId;
        });
        protected void addPackageContent(string packageId, string componentId, string productId) => addRandomRecord<PackageContentData>(d => {
            d.PackageId = packageId;
            d.ComponentId = componentId;
            d.ProductId = productId;
        });
        protected void addProductSet(string id) => addRandomRecord<ProductSetData>(id);
        protected void addProductSetContent(string setId, string productTypeId) =>
            addRandomRecord<ProductSetContentData>(d => {
                d.ProductSetId = setId;
                d.ProductTypeId = productTypeId;
            }
            );
        protected void addProductInclusion(string id) => addRandomRecord<ProductInclusionRuleData>(id);
        protected void addProductRelationshipType(string id) =>
            addRandomRecord<ProductRelationshipTypeData>(id);
        protected void addProductRelationship(string id) => addRandomRecord<ProductRelationshipData>(id);
        protected void addPrice(string id) => addRandomRecord<PriceData>(id);
        protected void addPriceEndorsement(string priceId, string partySignatureId) =>
            addRandomRecord<PriceEndorsementData>(d => {
                d.PriceId = priceId;
                d.PartySignatureId = partySignatureId;
            });
        protected void addPartySignature(string id) {
            var d = GetRandom.ObjectOf<PartySignatureData>();
            d.Id = id;
            add<IPartySignaturesRepo, PartySignature>(new PartySignature(d));
        }
    }
}
