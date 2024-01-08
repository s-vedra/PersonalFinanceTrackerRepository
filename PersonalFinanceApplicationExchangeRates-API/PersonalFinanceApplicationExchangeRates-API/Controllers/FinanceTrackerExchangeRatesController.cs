using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApplicationExchangeRates_API.Models;
using PersonalFinanceApplicationExchangeRates_API.RefitSettings;
using Refit;

namespace PersonalFinanceApplicationExchangeRates_API.Controllers
{
    [Route("api/exchange-rates")]
    [ApiController]
    public class FinanceTrackerExchangeRatesController : ControllerBase
    {
        private readonly IExchangeRatesClient _exchangeRatesClient;
        private readonly string _apiKey;
        public FinanceTrackerExchangeRatesController(IExchangeRatesClient exchangeRatesClient, IConfiguration configuration)
        {
            _exchangeRatesClient = exchangeRatesClient;
            _apiKey = configuration["ApiSettings:ApiKey"];
        }

        [HttpGet("latest-currencies/{base}")]
        public async Task<IActionResult> GetLatestCurrencies(string @base, [FromQuery] string? symbols)
        {
            try
            {
                var currencies = await _exchangeRatesClient.GetLatestCurrencies(_apiKey, @base.ToUpper(), symbols?.ToUpper());
                return Ok(currencies);
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("historical-currencies/{base}/{date}")]
        public async Task<IActionResult> GetHistoricalCurrencies(string @base, string date, [FromQuery] string? symbols)
        {
            try
            {
                var currencies = await _exchangeRatesClient.GetHistoricalCurrencies(_apiKey, @base.ToUpper(), date, symbols?.ToUpper());
                return Ok(currencies);
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("available-currencies/{type}")]
        public async Task<IActionResult> GetAvailableCurrencies(string type)
        {
            try
            {
                if(type.ToLower() == "fiat" || type.ToLower() == "crypto")
                {
                    var currencies = await _exchangeRatesClient.GetAvailableCurrencies(_apiKey, type);
                    return Ok(currencies);
                }
                throw new Exception($"Type can only be fiat or crypto, type: {type}");
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("convert")]
        public async Task<IActionResult> ConvertAmount(ExchangeAmountRequestDto model)
        {
            try
            {
                var conversion = await _exchangeRatesClient.ConvertAmount(_apiKey, model.From.ToUpper(), model.To.ToUpper(), model.Amount);
                return Ok(conversion);
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
