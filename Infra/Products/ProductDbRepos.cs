using Abc.Domain.Products;
using Abc.Domain.Products.Catalog;
using Abc.Domain.Products.Features;
using Abc.Domain.Products.Packets;
using Abc.Domain.Products.Price;
using Microsoft.Extensions.DependencyInjection;
using IPossibleFeatureValuesRepo = Abc.Domain.Products.Features.IPossibleFeatureValuesRepo;

namespace Abc.Infra.Products {

    public static class ProductDbRepos {

        public static void Register(IServiceCollection services) {
            services.AddTransient<IProductsRepo, ProductsRepo>();
            services.AddTransient<IProductTypesRepo, ProductTypesRepo>();
            services.AddTransient<IFeaturesRepo, FeaturesRepo>();
            services.AddTransient<IFeatureSpecsRepo, FeatureSpecsRepo>();
            services.AddTransient<IFeatureTypesRepo, FeatureTypesRepo>();
            services.AddTransient<IPossibleFeatureValuesRepo, PossibleFeatureValuesRepo>();
            services.AddTransient<IBatchesRepo, BatchesRepo>();
            services.AddTransient<IBatchCheckedByRepo, BatchCheckedByRepo>();
            services.AddTransient<IProductCatalogsRepo, ProductCatalogsRepo>();
            services.AddTransient<IProductCategoriesRepo, ProductCategoriesRepo>();
            services.AddTransient<ICatalogEntriesRepo, CatalogEntriesRepo>();
            services.AddTransient<ICatalogedProductsRepo, CatalogedProductsRepo>();
            services.AddTransient<IPackageContentsRepo, PackageContentsRepo>();
            services.AddTransient<IPackageComponentsRepo, PackageComponentsRepo>();
            services.AddTransient<IProductSetsRepo, ProductSetsRepo>();
            services.AddTransient<IProductSetContentsRepo, ProductSetContentsRepo>();
            services.AddTransient<IProductInclusionRulesRepo, ProductInclusionsRepo>();
            services.AddTransient<IProductRelationshipTypesRepo, ProductRelationshipTypesRepo>();
            services.AddTransient<IProductRelationshipsRepo, ProductRelationshipsRepo>();
            services.AddTransient<IPricesRepo, PricesRepo>();
            services.AddTransient<IPriceEndorsementsRepo, PriceEndorsementsRepo>();
            services.AddTransient<IContainerItemsRepo, ContainerItemsRepo>();
        }
    }
}

