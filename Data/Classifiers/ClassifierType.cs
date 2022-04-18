using System.ComponentModel;

namespace Abc.Data.Classifiers {
    public enum ClassifierType {
        Unspecified = 0,
        [Description("Order Identifier")] Order = 1,
        [Description("Capability")] PartyCapability = 2,
        [Description("Contact Usage")] ContactUsage = 3,
        [Description("Name Prefix")] PersonNamePrefix = 4,
        [Description("Registration Authority")] RegistrationAuthority = 5,
        [Description("Name Usage")] PartyNameUsage = 6,
        [Description("Registered Identifier")] RegisteredIdentifier = 7,
        [Description("Ethnicity")] PartyEthnicity = 8,
        [Description("Name Suffix")] PersonNameSuffix = 9,
        [Description("Organization Type")] Organization = 10,
        [Description("Preference")] PartyPreference = 11,
        [Description("Party Role in Order")] PartyRoleInOrder = 12,
        [Description("Process Priority")] ProcessPriority = 13,
        [Description("Action Status")] ActionStatus = 14,
        [Description("Sales Channel")] SalesChannel = 15,
        [Description("Terms and Conditions")] TermsAndConditions = 16,
        [Description("Taxation Type")] TaxationType = 17,
    }
}
