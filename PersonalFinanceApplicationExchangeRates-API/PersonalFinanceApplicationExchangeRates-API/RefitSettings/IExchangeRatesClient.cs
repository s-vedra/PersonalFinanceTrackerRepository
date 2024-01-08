using PersonalFinanceApplication_DtoModels.ResponseModels;
using PersonalFinanceApplicationExchangeRates_API.Models;
using PersonalFinanceApplicationTransfer_API.Models;
using Refit;

namespace PersonalFinanceApplicationExchangeRates_API.RefitSettings
{
    public interface IExchangeRatesClient
    {
        [Get("/latest")]
        public Task<ExchangeRateResponseDto> GetLatestCurrencies([Query] string api_key, [Query] string @base, [Query] string? symbols);

        [Get("/historical")]
        public Task<ExchangeRateResponseDto> GetHistoricalCurrencies([Query] string api_key, [Query] string @base, [Query] string date, [Query] string? symbols);

        [Get("/currencies")]
        public Task<CurrencyApiResponseDto> GetAvailableCurrencies([Query] string api_key, [Query] string type);

        [Get("/convert")]
        public Task<CurrencyConversionResponseDto> ConvertAmount([Query] string api_key, [Query] string from, [Query] string to, [Query] string amount);
    }
}
