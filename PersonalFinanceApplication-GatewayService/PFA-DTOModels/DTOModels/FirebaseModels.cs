namespace PFA_DTOModels.DTOModels
{
    public class TokenRequest
    {
        public string IdToken { get; set; }
    }

    public class TokenResponse
    {
        public TokenValidity TokenValidity { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
    }

    public enum TokenValidity
    {
        TokenValid = 1,
        TokenNotValid
    }

    public class FirebaseSettings
    {
        public string FirebaseEnvironmentVariable { get; set; }
    }
}
