using FirebaseAdmin.Auth;
using PFA_DTOModels.Commands;

namespace PFA_Services.Abstractions
{
    public interface IFirebaseAuthService
    {
        Task<UserRecord> GetCurrentUser(LoginRequestModels request);
        Task<string> FirebaseCustomToken(UserRecord request);
    }
}