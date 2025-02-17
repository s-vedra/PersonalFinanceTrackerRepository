using PFA_DTOModels.DTOModels;

namespace PFA_Services.Abstractions
{
    public interface IJwtAuthService
    {
        string GenerateToken(JwtRequest jwtRequest);
    }
}