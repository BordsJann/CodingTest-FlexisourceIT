using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Newtonsoft.Json;
using Services.RainfallService;

namespace CodingTest_FlexisourceIT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RainfallController : ControllerBase
    {
        private readonly IRainfallService _rainfallService;
        public RainfallController(IRainfallService rainfallService)
        {
            _rainfallService = rainfallService;
        }

        [HttpGet("{stationId}")]
        public async Task<IActionResult> GetRainfallReadingsByStation([FromRoute] string stationId, [FromQuery] int count = 10)
        {
            try
            {
                var res = await _rainfallService.GetRainfallByStation(stationId, count);
                return Ok(new ResponseDto
                {
                    RainfallReadingResponse = new RainfallReadingResponse
                    {
                        Description = "Get rainfall readings response",
                        Content = JsonConvert.SerializeObject(res)
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto
                {
                    ErrorResponse = new ErrorResponse
                    {
                        Description = "An error object returned for failed requests",
                        Content = ex.Message
                    }
                });
            }
        }
    }
}
