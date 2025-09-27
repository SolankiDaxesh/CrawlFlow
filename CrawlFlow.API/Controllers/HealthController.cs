using Microsoft.AspNetCore.Mvc;
using CrawlFlow.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CrawlFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly MongoContext _mongoContext;
        private readonly PostgreSqlContext _postgreSqlContext;

        public HealthController(MongoContext mongoContext, PostgreSqlContext postgreSqlContext)
        {
            _mongoContext = mongoContext;
            _postgreSqlContext = postgreSqlContext;
        }

        [HttpGet]
        public IActionResult Get() => Ok("API is running");

        [HttpGet("mongo")]
        public IActionResult CheckMongo()
        {
            try
            {
                var collections = _mongoContext.GetCollection<BsonDocument>("test").Database.ListCollectionNames().ToList();
                return Ok(new { status = "success", collections });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpGet("postgres")]
        public async Task<IActionResult> CheckPostgres()
        {
            try
            {
                // Simple query to check connection
                var canConnect = await _postgreSqlContext.Database.CanConnectAsync();
                return Ok(new { status = canConnect ? "success" : "failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }
    }
}
