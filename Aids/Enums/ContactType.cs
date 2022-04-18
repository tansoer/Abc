using System.ComponentModel;

namespace Abc.Aids.Enums {
    public enum ContactType {
        [Description("Unspecified")] Unspecified = 0,
        [Description("Email Address")] Email = 1,
        [Description("Web Address")] Web = 2,
        [Description("Phone Number")] Telecom = 3,
        [Description("Postal Address")] Postal = 4
    }

    public static class ContactTypeExtensions {
        public static bool IsPostal(this ContactType t) => t == ContactType.Postal;
        public static bool IsTelecom(this ContactType t) => t == ContactType.Telecom;
        public static bool IsWeb(this ContactType t) => t == ContactType.Web;
        public static bool IsEmail(this ContactType t) => t == ContactType.Email;
    }
}