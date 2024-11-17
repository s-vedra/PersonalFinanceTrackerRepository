using PersonalFinanceApplication_DTO.RequestModels;
using PersonalFinanceApplication_DTO.ResponseModels;
using Refit;

namespace PersonalFinanceApplication_API.ReffitSettings
{
    public interface IProxyApi
    {
        [Get("/api/exchange-rates/latest-currencies/{base}")]
        public Task<ExchangeRateResponseDto> GetLatestCurrencies(string @base, [Query] string? symbols);

        [Post("/api/exchange-rates/historical-currencies")]
        public Task<ExchangeRateResponseDto> GetHistoricalCurrencies(HistoricalCurrenciesRequestDto model);

        [Get("/api/exchange-rates/available-currencies/{type}")]
        public Task<CurrencyApiResponseDto> GetAvailableCurrencies(string type);

        [Post("/api/exchange-rates/convert")]
        public Task<CurrencyConversionResponseDto> ConvertAmount(ExchangeAmountRequestDto request);

        [Get("/api/exchange-rates/currencies")]
        public Task<Dictionary<string,string>> GetAllCurrencies();
    }
}
