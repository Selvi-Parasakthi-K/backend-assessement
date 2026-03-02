using backend_assessement.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_assessement.Controllers
{
    [ApiController]
    [Route("")]
    public class SalesApiController : ControllerBase
    {
        public readonly SalesApiServices _salesApiServices;

        public SalesApiController(SalesApiServices salesApiServices)
        {
            _salesApiServices = salesApiServices;
        }

        [HttpPost("init")]
        public IActionResult InitInventory([FromQuery] int count)
        {
            _salesApiServices.InitInventory(count);
            return Ok(new
            {
                Status = 200,
                Message = $"Inventory initialized with {count} items."
            });
        }

        [HttpPost("reserve")]
        public async Task<IActionResult> Reserve([FromQuery] string userId)
        {
            var result = await _salesApiServices.Reserve(userId);
            var status = (int)result.GetType().GetProperty("Status").GetValue(result, null);
            return StatusCode(status);
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(_salesApiServices.getStatus());
        }
    }
}
