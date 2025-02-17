using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using PFA_DTOModels.Commands;
using PFA_DTOModels.DTOModels;
using PFA_Services.Abstractions;


namespace PFA_Services.HelperMethods
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        public async Task<UserRecord> GetCurrentUser(LoginRequestModels request)
        {
            var user = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(request.Email);
            if (user is null)
                throw new ArgumentException($"Wrong credentials specified");
            return user;
        }

        public async Task<string> FirebaseCustomToken(UserRecord request)
        {
            return await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(request.Uid);
        }

        public static void InitializeFirebase(FirebaseSettings firebaseSettings)
        {
            try
            {
                string credentialsPath = Environment.GetEnvironmentVariable(firebaseSettings.FirebaseEnvironmentVariable, EnvironmentVariableTarget.User);
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(credentialsPath)
                });
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Firebase initialization failed: {ex.Message}");
            }
        }

        public static async Task<TokenResponse> IsValidToken(TokenRequest tokenRequest)
        {
            if (string.IsNullOrEmpty(tokenRequest.IdToken))
                throw new ArgumentException("Token is empty");

            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(tokenRequest.IdToken);
            var userId = decodedToken.Uid;
            var email = decodedToken.Claims.ContainsKey("email") ? decodedToken.Claims["email"].ToString() : string.Empty;

            return new TokenResponse()
            {
                TokenValidity = TokenValidity.TokenValid,
                Email = email,
                UserId = userId
            };
        }
    }
}
