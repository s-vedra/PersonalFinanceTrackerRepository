﻿using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApplication_Services.CommandHandlers;
using PersonalFinanceApplication_Services.CommandHandlers.ExpenseCommandHandlers;
using PersonalFinanceApplication_Services.CommandHandlers.ExpenseCommands;
using PersonalFinanceApplication_Services.QueryHandlers.ExpenseQueryHandlers;

namespace Personal_Finance_Application_API.Controllers
{
    [Route("api/dashboard/expenses")]
    [ApiController]
    public class FinanceTrackerExpenseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FinanceTrackerExpenseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("expenditures")]
        public async Task<IActionResult> GetAllExpenditures()
        {
            try
            {
                var expenditures = await _mediator.Send(new GetExpensesQuery());
                return Ok(expenditures);
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

        [HttpGet("expenditure/{id}")]
        public async Task<IActionResult> GetExpenditure(int id)
        {
            try
            {
                var expenditure = await _mediator.Send(new GetExpenseQuery() { Id = id });
                return Ok(expenditure);
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

        [HttpPost("expenditure")]
        public async Task<IActionResult> AddExpenditure(CreateExpenseCommand expenseDto)
        {
            try
            {
                await _mediator.Send(expenseDto);
                return Ok("Expense added");
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

        [HttpPut("expenditure/update")]
        public async Task<IActionResult> UpdateExpenditure(UpdateExpenseCommand expenseDto)
        {
            try
            {
                await _mediator.Send(expenseDto);
                return Ok("Expense updated");
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

        [HttpDelete("expenditure/{id}")]
        public async Task<IActionResult> DeleteExpenditure(int id)
        {
            try
            {
                await _mediator.Send(new DeleteExpenseCommand() { Id = id });
                return Ok("Expense deleted");
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
