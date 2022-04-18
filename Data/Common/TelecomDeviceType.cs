
using System.ComponentModel;

namespace Abc.Data.Common {
    public enum TelecomDeviceType {
        [Description("Unspecified Device")] NotKnown = 0,
        [Description("Phone Device")] Phone = 1,
        [Description("Fax Device")] Fax = 2,
        [Description("Mobile Device")] Mobile = 3,
        [Description("Pager Device")] Pager = 4
    }
}


