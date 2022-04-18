using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products.Packets {

    public sealed class Package : BaseProduct<PackageType> {

        private static string packageId => GetMember.Name<PackageContentData>(x => x.PackageId);

        public Package(ProductData d = null) : base(d) { }

        public override PackageType Type => type as PackageType;

        public IReadOnlyList<PackageContent> PackageContents =>
            new GetFrom<IPackageContentsRepo, PackageContent>().ListBy(packageId, Id);

        public IReadOnlyList<IProduct> Contents =>
            PackageContents.Select(x => x.Product).ToList().AsReadOnly();

    }

}