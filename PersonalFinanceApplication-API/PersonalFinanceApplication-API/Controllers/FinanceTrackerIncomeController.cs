using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApplication_Services.CommandHandlers.IncomeCommandHandlers;
using PersonalFinanceApplication_Services.QueryHandlers.IncomeAndBalanceQueryHandlers;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceApplication_API.Controllers
{
    [Route("api/dashboard/income")]
    [ApiController]
    public class FinanceTrackerIncomeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FinanceTrackerIncomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("balance/{currency}")]
        public async Task<IActionResult> GetBalance(string currency)
        {
            try
            {
                var balance = await _mediator.Send(new GetBalanceQuery() { Currency = currency});
                return Ok(balance);
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

        [HttpGet("incomes")]
        public async Task<IActionResult> GetAllIncomes()
        {
            try
            {
                var incomes = await _mediator.Send(new GetIncomesQuery());
                return Ok(incomes);
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

        [HttpGet("income/{id}")]
        public async Task<IActionResult> GetIncome(int id)
        {
            try
            {
                var income = await _mediator.Send(new GetIncomeQuery() { Id = id });
                return Ok(income);
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


        [HttpPost("income")]
        public async Task<IActionResult> AddIncome(CreateIncomeCommand incomeDto)
        {
            try
            {
                await _mediator.Send(incomeDto);
                return Ok("Income added");
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

        [HttpPut("income/update")]
        public async Task<IActionResult> UpdateIncome(UpdateIncomeCommand incomeDto)
        {
            try
            {
                await _mediator.Send(incomeDto);
                return Ok("Income updated");
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

        [HttpDelete("income/{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            try
            {
                await _mediator.Send(new DeleteIncomeCommand() { Id = id });
                return Ok("Income deleted");
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
