using Inventory.Shared;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Inventory.Server.Services
{
    public class FoodService
    {
        private readonly IMongoCollection<Food> _foods;

        public FoodService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _foods = database.GetCollection<Food>("Food"); // 👈 collection name
        }

        public async Task<List<Food>> GetAllAsync() =>
            await _foods.Find(_ => true).ToListAsync();

        public async Task<Food> CreateAsync(Food food)
        {
            await _foods.InsertOneAsync(food);
            return food;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _foods.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }
    }
}

