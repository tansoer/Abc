using Abc.Data.Common;

namespace Abc.Data.Products {

    public sealed class PriceEndorsementData :EntityBaseData {

        public string PriceId { get; set; }
        public string PartySignatureId { get; set; }

    }

}