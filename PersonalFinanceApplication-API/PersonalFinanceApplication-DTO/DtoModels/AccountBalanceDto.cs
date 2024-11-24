namespace PersonalFinanceApplication_DTO.DtoModels
{
    public class AccountBalanceDto
    {
        public int AccountBalanceId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime LastDateAddedMoney { get; set; }
        public DateTime LastDateDrawMoney { get; set; }
        public int UserContractId { get; set; }
    }
}
