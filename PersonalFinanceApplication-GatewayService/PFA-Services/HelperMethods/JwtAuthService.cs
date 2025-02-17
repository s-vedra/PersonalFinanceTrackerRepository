using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PFA_DTOModels.DTOModels;
using PFA_Services.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PFA_Services.HelperMethods
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly JwtSettings _jwtSettings;
        public JwtAuthService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(JwtRequest jwtRequest)
        {
            var secretKey = Environment.GetEnvironmentVariable(_jwtSettings.SecretKey, EnvironmentVariableTarget.User);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwtRequest.UserId),
                new Claim(JwtRegisteredClaimNames.Email, jwtRequest.Email),
                new Claim("role", "User")
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
