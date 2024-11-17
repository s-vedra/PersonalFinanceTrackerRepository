using Microsoft.AspNetCore.Mvc;

namespace PersonalFinanceApplication_AnalyzingService.Controllers
{
    [Route("api/analyzer")]
    [ApiController]
    public class AnalyzingServiceController : ControllerBase
    {
        [HttpGet("available-currencies/{type}")]
        public async Task<IActionResult> GetAvailableCurrencies(string type)
        {
            try
            {
                var result = "test";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
