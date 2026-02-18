using PersonalFinanceApplication_DomainModels.Models;
using PersonalFinanceApplication_DTO.DtoModels;

namespace PersonalFinanceApplication_Mappers.Mappers
{
    public static class SalarySchedulerMapper
    {
        public static SalarySchedulerDto ToDto(this SalaryScheduler salaryScheduler)
        {
            return new SalarySchedulerDto
            {
                ReferenceId = salaryScheduler.ReferenceId,
                Amount = salaryScheduler.Amount,
                DayOfMonth = salaryScheduler.DayOfMonth,
                IsActive = salaryScheduler.IsActive,
                LastExecutedAt = salaryScheduler.LastExecutedAt,
                Notes = salaryScheduler.Notes,
                SalarySchedulerId = salaryScheduler.SalarySchedulerId,
                UserContractId = salaryScheduler.UserContractId,
                Currency = salaryScheduler.Currency,
                Created = salaryScheduler.Created
            };
        }

        public static SalaryScheduler ToModel(this SalarySchedulerDto salaryScheduler)
        {
            return new SalaryScheduler
            {
                ReferenceId = salaryScheduler.ReferenceId,
                Amount = salaryScheduler.Amount,
                DayOfMonth = salaryScheduler.DayOfMonth,
                IsActive = salaryScheduler.IsActive,
                LastExecutedAt = salaryScheduler.LastExecutedAt,
                Notes = salaryScheduler.Notes,
                SalarySchedulerId = salaryScheduler.SalarySchedulerId,
                UserContractId = salaryScheduler.UserContractId,
                Currency = salaryScheduler.Currency,
                Created = salaryScheduler.Created
            };
        }
    }
}
