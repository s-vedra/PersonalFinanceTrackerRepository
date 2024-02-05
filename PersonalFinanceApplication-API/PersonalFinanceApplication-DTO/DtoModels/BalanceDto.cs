namespace PersonalFinanceApplication_DTO.DtoModels
{
    public class BalanceDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime LastDateAddedMoney { get; set; }
        public DateTime LastDateDrawMoney { get; set; }
    }
}
