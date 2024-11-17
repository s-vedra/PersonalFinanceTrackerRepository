using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalFinanceApplication_DtoModels.RequestModels;
using PersonalFinanceApplicationExchangeRates_API.Models;
using PersonalFinanceApplicationExchangeRates_API.RefitSettings;
using PFA_Services.RequestService;
using Refit;

namespace PersonalFinanceApplicationExchangeRates_API.Controllers
{
    [Route("api/exchange-rates")]
    [ApiController]
    public class FinanceTrackerExchangeRatesController : ControllerBase
    {
        private readonly IExchangeRatesClient _exchangeRatesClient;
        private readonly ICurrenciesClient _currenciesClient;
        private readonly string _apiKey;
        private readonly IRequestService _requestService;
        private readonly IList<string> _availableTypes;
        public FinanceTrackerExchangeRatesController(IExchangeRatesClient exchangeRatesClient, ICurrenciesClient currenciesClient, IConfiguration configuration, IRequestService requestService)
        {
            _exchangeRatesClient = exchangeRatesClient;
            _currenciesClient = currenciesClient;
            _apiKey = configuration["ApiSettings:ApiKey"];
            _availableTypes = configuration.GetSection("ApiSettings:AvailableCurrencyTypes").Get<string[]>();
            _requestService = requestService;
        }

        [HttpGet("latest-currencies/{base}")]
        public async Task<IActionResult> GetLatestCurrencies(string @base, [FromQuery] string? symbols)
        {
            try
            {
                _requestService.ValidateRequest(@base);
                var currencies = await _exchangeRatesClient.GetLatestCurrencies(_apiKey, @base.ToUpper(), symbols?.ToUpper());
                return Ok(currencies);
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("historical-currencies")]
        public async Task<IActionResult> GetHistoricalCurrencies(HistoricalCurrenciesRequestDto model)
        {
            try
            {
                _requestService.ValidateRequest(model.Base);
                _requestService.ValidateRequest(model.Date);
                var currencies = await _exchangeRatesClient.GetHistoricalCurrencies(_apiKey, model.Base.ToUpper(), model.Date, model.Symbols?.ToUpper());
                return Ok(currencies);
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("available-currencies/{type}")]
        public async Task<IActionResult> GetAvailableCurrencies(string type)
        {
            try
            {
                _requestService.ValidateType(type, _availableTypes);
                var currencies = await _exchangeRatesClient.GetAvailableCurrencies(_apiKey, type);
                return Ok(currencies);
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("convert")]
        public async Task<IActionResult> ConvertAmount(ExchangeAmountRequestDto model)
        {
            try
            {
                _requestService.ValidateRequest(model.From);
                _requestService.ValidateRequest(model.To);
                _requestService.ValidateRequest(model.Amount);
                var conversion = await _exchangeRatesClient.ConvertAmount(_apiKey, model.From.ToUpper(), model.To.ToUpper(), model.Amount);
                return Ok(conversion);
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("currencies")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            try
            {
                var currencies = JsonConvert.DeserializeObject<Dictionary<string, string>>(await _currenciesClient.GetAllCurrencies());
                return Ok(currencies);
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
