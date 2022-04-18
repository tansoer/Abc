using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Products {
    public sealed class PriceEndorsementView :EntityBaseView {
        [DisplayName("Price")]
        public string PriceId { get; set; }
        [DisplayName("Party Signature")]
        public string PartySignatureId { get; set; }
    }
}
