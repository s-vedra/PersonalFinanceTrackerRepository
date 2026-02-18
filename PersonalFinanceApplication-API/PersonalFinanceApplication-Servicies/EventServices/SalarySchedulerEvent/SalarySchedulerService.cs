
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Services.CommandHandlers.IncomeCommandHandlers;

namespace PersonalFinanceApplication_Services.EventServices.SalarySchedulerEvent
{
    public class SalarySchedulerService : ISalarySchedulerService
    {
        private readonly IScheduledSalaryRepository _schedulerSalaryRepository;
        private readonly IMediator _mediator;
        public SalarySchedulerService(IScheduledSalaryRepository schedulerSalaryRepository, IMediator mediator)
        {
            _schedulerSalaryRepository = schedulerSalaryRepository;
            _mediator = mediator;
        }

        public async Task ProcessMonthlySalariesAsync()
        {
            var today = DateTime.Today;
            var maxDay = DateTime.DaysInMonth(today.Year, today.Month);

            var salaries = _schedulerSalaryRepository
                .GetActiveSalarySchedulers()
                .ToList();

            foreach (var salary in salaries)
            {
                if (!ShouldRunToday(salary, today, maxDay))
                    continue;

                if (AlreadyExecutedThisMonth(salary, today))
                    continue;

                await CreateSalaryIncomeAsync(salary, today);

                salary.LastExecutedAt = today;
                _schedulerSalaryRepository.UpdateEntity(salary, salary);
            }
        }

        private static bool ShouldRunToday(SalaryScheduler salary, DateTime today, int maxDay)
        {
            return salary.DayOfMonth == today.Day
                   || (salary.DayOfMonth > maxDay && today.Day == maxDay);
        }

        private static bool AlreadyExecutedThisMonth(SalaryScheduler salary, DateTime today)
        {
            if (!salary.LastExecutedAt.HasValue)
                return false;

            var last = salary.LastExecutedAt.Value;

            return last.Month == today.Month && last.Year == today.Year;
        }

        private async Task CreateSalaryIncomeAsync(SalaryScheduler salary, DateTime today)
        {
            var command = new CreateIncomeCommand
            {
                IncomeDto = new IncomeDto
                {
                    Amount = salary.Amount,
                    Category = IncomeCategory.Salary,
                    Currency = salary.Currency,
                    Date = today,
                    PaymentIssue = PaymentIssue.Card,
                    UserContractId = salary.UserContractId,
                    ReferenceId = Guid.NewGuid(),
                    Purpose = "Salary-Scheduler"
                }
            };

            await _mediator.Send(command);
        }
    }
}
