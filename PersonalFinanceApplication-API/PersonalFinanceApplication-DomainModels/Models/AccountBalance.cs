﻿namespace PersonalFinanceApplication_DomainModels.Models
{
    public class AccountBalance
    {
        public int AccountBalanceId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime LastDateAddedMoney { get; set; }
        public DateTime LastDateDrawMoney { get; set; }
    }
}