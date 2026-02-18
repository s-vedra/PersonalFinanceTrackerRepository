using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApplication_Services.CommandHandlers.UserContractCommandHandlers;
using PersonalFinanceApplication_Services.QueryHandlers.UserContractQueryHandlers;

namespace PersonalFinanceApplication_API.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    //[Authorize]
    public class FinanceTrackerUserContractController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FinanceTrackerUserContractController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("user-contract/balance/{id}")]
        public async Task<IActionResult> GetAccountBalancePerContract(int id)
        {
            try
            {
                var accountBalance = await _mediator.Send(new GetBalanceQuery() { UserContractId = id });
                return Ok(accountBalance);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
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
                await _mediator.Send(userContractDto);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
