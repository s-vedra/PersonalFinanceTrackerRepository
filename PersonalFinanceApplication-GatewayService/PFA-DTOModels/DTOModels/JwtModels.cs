namespace PFA_DTOModels.DTOModels
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }

    public class JwtRequest
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
