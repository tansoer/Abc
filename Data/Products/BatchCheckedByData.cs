using Abc.Data.Common;

namespace Abc.Data.Products {

    public sealed class BatchCheckedByData :EntityBaseData {

        public string BatchId { get; set; }
        public string PartySignatureId { get; set; }

    }

}
