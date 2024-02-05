using Refit;

namespace PersonalFinanceApplicationExchangeRates_API.RefitSettings
{
    public interface ICurrenciesClient
    {
        [Get("/currencies.json")]
        public Task<string> GetAllCurrencies();
    }
}
