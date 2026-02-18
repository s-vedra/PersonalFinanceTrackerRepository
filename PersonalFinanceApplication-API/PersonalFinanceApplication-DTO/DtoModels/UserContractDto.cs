using PersonalFinanceApplication_DomainModels.Enums;

namespace PersonalFinanceApplication_DTO.DtoModels
{
    public class UserContractDto
    {
        public int UserContractId { get; set; }
        public AccountBalanceDto AccountBalance { get; set; }
        public string ContractName { get; set; }
        public ContractType ContractType { get; set; }
        public ICollection<IncomeDto> Incomes { get; set; }
        public ICollection<ExpenseDto> Expenses { get; set; }
        public int UserId { get; set; }
        public UserContractStatus UserContractStatus { get; set; }
        public DateTime DateOpened { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }
}
