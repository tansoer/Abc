using Abc.Aids.Enums;
using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Parties {
    public sealed class PartyView : EntityBaseView {
        //TODO Description is not in PartyData, is it necessary?
        public string Description { get; set; }
        [DisplayName("Primary Name")] public new string Name { get; set; }
        public IsoGender Gender { get; set; }
        [DisplayName("Party Type")] public PartyType PartyType { get; set; }
        [DisplayName("Organization Type")]
        public string OrganizationTypeId { get; set; }
        public bool IsPerson() => PartyType == PartyType.Person;
        public bool IsOrganization() => PartyType == PartyType.Organization;
        public override string ToString() => new PartyViewFactory().Create(this).ToString();
    }
}
