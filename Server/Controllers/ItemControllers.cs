using Inventory.Shared;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Inventory.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IMongoCollection<Item> _items;

        public ItemController(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoConnection"));
            var db = client.GetDatabase("EmployeeDB"); // your database name
            _items = db.GetCollection<Item>("Example"); // your collection name
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAll()
        {
            var items = await _items.Find(_ => true).ToListAsync();
            return Ok(items);
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Item>> Create(Item item)
        {
            // MongoDB generates Id automatically
            await _items.InsertOneAsync(item);

            // Return the item including the generated Id
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _items.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount == 0) return NotFound();
            return NoContent();
        }

  
    }
}
