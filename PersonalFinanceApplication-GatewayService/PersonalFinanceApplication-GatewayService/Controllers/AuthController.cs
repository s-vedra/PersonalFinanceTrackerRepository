using MediatR;
using Microsoft.AspNetCore.Mvc;
using PFA_Services.CommandHandlers;

namespace PersonalFinanceApplication_GatewayService.Controllers
{
    [Route("api/gateway-service")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestCommand request)
        {
            try
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = "Invalid credentials", Error = ex.Message });
            }

        }

        //[HttpPost("verify-login-token")]
        //public async Task<IActionResult> VerifyLoginToken(TokenRequest request)
        //{
        //    try
        //    {
        //        var tokenResponse = await FirebaseAuthService.IsValidToken(request);
        //        return Ok(tokenResponse);
        //    }
        //    catch
        //    {
        //        return Unauthorized("Invalid token");
        //    }
        //}
    }
}
