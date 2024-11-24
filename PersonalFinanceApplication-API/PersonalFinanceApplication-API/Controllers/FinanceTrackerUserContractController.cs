using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApplication_Services.CommandHandlers.UserContractCommandHandlers;
using PersonalFinanceApplication_Services.QueryHandlers.UserContractQueryHandlers;

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

        [HttpGet]
        public async Task<IActionResult> GetAccountBalancePerContract(int id)
        {
            try
            {
                var accountBalance = await _mediator.Send(new GetBalanceQuery() { UserContractId = id });
                return Ok(accountBalance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("user-contract")]
        public async Task<IActionResult> CreateUserContract(CreateUserContractCommand userContractDto)
        {
            try
            {
                var userContract = await _mediator.Send(userContractDto);
                return Ok(userContract);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
