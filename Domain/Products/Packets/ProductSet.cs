using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Methods;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products.Packets {

    public sealed class ProductSet : Entity<ProductSetData> {
        private static string setId => GetMember.Name<ProductSetContent>(x => x.ProductSetId);
        public ProductSet() : this(null) { }
        public ProductSet(ProductSetData d) : base(d) { }
        public string PackageTypeId => Data?.PackageTypeId ?? Unspecified.String;
        public IReadOnlyList<ProductSetContent> RelatedTypes =>
            new GetFrom<IProductSetContentsRepo, ProductSetContent>().ListBy(setId, Id);
        public IReadOnlyList<IProductType> Contents => RelatedTypes.Select(x => x.ProductType).ToList().AsReadOnly();
        public int NumberOfElements => Contents.Count;
        public IProductType GetElement(int i) {
            var error = new ProductTypeError();
            return Safe.Run(
                () => {
                    if (i < 0) return error;
                    return i >= Contents.Count ? error : Contents[i];
                }, error);
        }
        public bool IsSubset(IReadOnlyList<IProductType> set)
            => set is null || set.All(t => Contents.Any(x => x.Id == t.Id));
        public bool IsSubset(ProductSet set) => IsSubset(set?.Contents);
        public bool ContainsAny(IReadOnlyList<IProduct> products)
            => products?.Any(p => Contents.Any(t => p.TypeId == t.Id)) ?? false;
        public override string ToString()
            => "{" + getContentsAsString() + "}";
        private string getContentsAsString() {
            string s = "";
            if (Contents.Count == 0) return s;
            foreach (var e in Contents) {
                s += $"{e.Name}, ";
            }
            return s[0..^2];
        }
    }
}
