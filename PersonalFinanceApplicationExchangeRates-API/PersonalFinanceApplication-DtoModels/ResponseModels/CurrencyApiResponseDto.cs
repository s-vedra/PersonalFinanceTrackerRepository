namespace PersonalFinanceApplication_DtoModels.ResponseModels
{
    public class CurrencyApiResponseDto
    {
        public List<CurrencyInfo> Response { get; set; }
    }

    public class CurrencyInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Short_Code { get; set; }
        public string Symbol { get; set; }
    }
}
