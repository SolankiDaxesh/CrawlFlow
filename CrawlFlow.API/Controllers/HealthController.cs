using Microsoft.AspNetCore.Mvc;

namespace CrawlFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("API is running");
    }
}
