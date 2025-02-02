using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApplication_GatewayService.FirebaseService;
using PersonalFinanceApplication_GatewayService.Models;

namespace PersonalFinanceApplication_GatewayService.Controllers
{
    [Route("api/gateway-service")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("verify-login-token")]
        public async Task<IActionResult> VerifyLoginToken([FromBody] TokenRequest request)
        {
            try
            {
                var tokenResponse = await FirebaseAuthService.IsValidToken(request);
                return Ok(tokenResponse);
            }
            catch
            {
                return Unauthorized("Invalid token");
            }
        }
    }


}
