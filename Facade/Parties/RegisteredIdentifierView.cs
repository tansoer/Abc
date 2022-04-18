using Abc.Facade.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abc.Facade.Parties {
    public sealed class RegisteredIdentifierView : PartyAttributeView {
        [DisplayName("Registered identifier type")]
        public string RegisteredIdentifierTypeId { get; set; }
        [DisplayName("Registration authority")]
        public string RegistrationAuthorityId { get; set; }
        [DisplayName("Comments")]
        public new string Details { get; set; }
        [Required] [DisplayName("Identifier")] public new string Name { get; set; }
    }
}
