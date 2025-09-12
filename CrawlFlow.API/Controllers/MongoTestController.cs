using Microsoft.AspNetCore.Mvc;
using CrawlFlow.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;


namespace CrawlFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MongoTestController : ControllerBase
    {
        private readonly MongoContext _mongoContext;

        public MongoTestController(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            try
            {
                // Try to list collections as a simple connectivity test
                var collections = _mongoContext.GetCollection<BsonDocument>("test").Database.ListCollectionNames().ToList();
                return Ok(new { status = "success", collections });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }
    }
}