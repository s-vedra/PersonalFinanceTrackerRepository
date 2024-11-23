using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApplication_Services.CommandHandlers.UserContractCommandHandlers;

namespace PersonalFinanceApplication_API.Controllers
{
    [Route("api/user-contract")]
    [ApiController]
    public class FinanceTrackerUserContractController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FinanceTrackerUserContractController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("user-contract")]
        public async Task<IActionResult> CreateUserContract(CreateUserContractCommand userContract)
        {
            try
            {
                var balance = await _mediator.Send(userContract);
                return Ok(balance);
            }
            //catch (ValidationException ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
