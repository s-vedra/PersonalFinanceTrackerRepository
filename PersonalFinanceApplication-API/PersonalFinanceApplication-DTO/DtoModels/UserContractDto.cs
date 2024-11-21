using PersonalFinanceApplication_DomainModels.Enums;

namespace PersonalFinanceApplication_DTO.DtoModels
{
    public class UserContractDto
    {
        public int UserContractId { get; set; }
        public int AccountBalanceId { get; set; }
        public ContractType ContractType { get; set; }
        public ICollection<IncomeDto> Incomes { get; set; }
        public ICollection<ExpenseDto> Expenses { get; set; }
        public int UserId { get; set; }
    }
}
