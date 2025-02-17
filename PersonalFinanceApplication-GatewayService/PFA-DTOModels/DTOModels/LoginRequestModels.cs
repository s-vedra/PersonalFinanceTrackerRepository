namespace PFA_DTOModels.Commands
{
    public class LoginRequestModels
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseModel
    {
        public string FirebaseToken { get; set; }
        public string InternalJwt { get; set; }
    }
}
