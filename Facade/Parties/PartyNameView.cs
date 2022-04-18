using System.ComponentModel;
using Abc.Aids.Enums;
using Abc.Facade.Common;
using System.ComponentModel.DataAnnotations;

namespace Abc.Facade.Parties {
    public sealed class PartyNameView : EntityBaseView {
        public static string FamilyNameCaption => "Family Name";
        [DisplayName("First Name")] public string GivenName { get; set; }
        [DisplayName("Middle Name")] public string MiddleName { get; set; }
        [DisplayName("Preferred Name")] public string PreferredName { get; set; }
        [DisplayName("Party Type")] public PartyType PartyType { get; set; }
        [DisplayName("Name Type")] public NameType NameType { get; set; }
        public string PartyId { get; set; }
        public bool IsPersonName() => PartyType == PartyType.Person;
        public bool IsOrganizationName() => PartyType == PartyType.Organization;
        public override string ToString() => new PartyNameViewFactory().Create(this).ToString();
    }
}
