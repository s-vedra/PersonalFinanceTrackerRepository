using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApplication_Services.CommandHandlers.SalarySchedulerHandlers;
namespace PersonalFinanceApplication_API.Controllers
{
    [Route("api/dashboard/salary-schedulers")]
    [ApiController]
    public class FinanceTrackerSalarySchedulerController : ControllerBase
    {

        private readonly IMediator _mediator;
        public FinanceTrackerSalarySchedulerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("salary-scheduler")]
        public async Task<IActionResult> CreateSalaryScheduler(CreateSalarySchedulerCommand salarySchedulerCommand)
        {
            try
            {
                var incomes = await _mediator.Send(salarySchedulerCommand);
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

    }
}
