using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApplication_API.RefitSettings;
using PersonalFinanceApplication_DTO.RequestModels;
using Refit;

namespace PersonalFinanceApplication_API.Controllers
{
    [Route("api/exchange-rates")]
    [ApiController]
    public class FinanceTrackerExchangeRatesController : ControllerBase
    {
        private readonly IProxyApi _proxyApi;
        public FinanceTrackerExchangeRatesController(IProxyApi proxyApi)
        {
            _proxyApi = proxyApi;
        }

        [HttpGet("latest-currencies/{base}")]
        public async Task<IActionResult> GetLatestCurrencies(string @base, [Query] string? symbols)
        {
            try
            {
                var result = await _proxyApi.GetLatestCurrencies(@base, symbols);
                return Ok(result);
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
        public async Task<IActionResult> GetHistoricalCurrencies(HistoricalCurrenciesRequestDto request)
        {
            try
            {
                var result = await _proxyApi.GetHistoricalCurrencies(request);
                return Ok(result);
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
                var result = await _proxyApi.GetAvailableCurrencies(type);
                return Ok(result);
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
        public async Task<IActionResult> ConvertAmount(ExchangeAmountRequestDto request)
        {
            try
            {
                var result = await _proxyApi.ConvertAmount(request);
                return Ok(result);
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
                var result = await _proxyApi.GetAllCurrencies();
                return Ok(result);
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
