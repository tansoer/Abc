using Abc.Data.Common;

namespace Abc.Data.Products {

    public sealed class PackageContentData :EntityBaseData {

        public string ProductId { get; set; }
        public string PackageId { get; set; }
        public string ComponentId { get; set; }
    }

}
