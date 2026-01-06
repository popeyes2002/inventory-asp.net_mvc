using Inventory.Shared;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Inventory.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly IMongoCollection<Food> _foods;

        public FoodController(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoConnection"));
            var db = client.GetDatabase("EmployeeDB"); // 👈 same db
            _foods = db.GetCollection<Food>("Food");    // 👈 Food collection
        }

        [HttpGet]
        public async Task<ActionResult<List<Food>>> GetAll()
        {
            var foods = await _foods.Find(_ => true).ToListAsync();
            return Ok(foods);
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Food>> Create(Food food)
        {
            await _foods.InsertOneAsync(food);
            return Ok(food);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _foods.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount == 0) return NotFound();
            return NoContent();
        }
    }
}
