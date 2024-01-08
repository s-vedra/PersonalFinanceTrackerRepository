namespace PersonalFinanceApplication_DTO.ResponseModels
{
    public class CurrencyApiResponseDto
    {
        public Meta Meta { get; set; }
        public List<CurrencyInfo> Response { get; set; }
    }

    public class Meta
    {
        public int Code { get; set; }
        public string Disclaimer { get; set; }
    }

    public class CurrencyInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Short_Code { get; set; }
        public string Code { get; set; }
        public int Precision { get; set; }
        public int Subunit { get; set; }
        public string Symbol { get; set; }
        public bool Symbol_First { get; set; }
        public string Decimal_Mark { get; set; }
        public string Thousands_Separator { get; set; }
    }
}
