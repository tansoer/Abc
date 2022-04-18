namespace Abc.Data.Parties {

    public sealed class AuthenticationData : PartyAttributeData {
        public string PartyUserId { get; set; }
        public string Token { get; set; }
        public string AuthenticationProvider { get; set; }
    }
}
