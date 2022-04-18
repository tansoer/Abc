using Abc.Data.Common;

namespace Abc.Data.Products {

    public sealed class PackageComponentData :EntityBaseData {

        public string ProductTypeId { get; set; }
        public string PackageTypeId { get; set; }

    }

}