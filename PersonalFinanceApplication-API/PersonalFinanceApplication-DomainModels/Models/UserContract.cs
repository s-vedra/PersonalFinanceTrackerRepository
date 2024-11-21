﻿using PersonalFinanceApplication_DomainModels.Enums;

namespace PersonalFinanceApplication_DomainModels.Models
{
    public class UserContract
    {
        public int UserContractId { get; set; }
        public int AccountBalanceId { get; set; }
        public ContractType ContractType { get; set; }
        public ICollection<Income> Incomes { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
