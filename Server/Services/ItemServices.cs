using Inventory.Shared;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Formats.Asn1;
using System.Threading.Tasks;

namespace Inventory.Server.Services
{
    // Service to manage Items
    public class ItemService
    {
        private readonly IMongoCollection<Item> _items;

        public ItemService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _items = database.GetCollection<Item>(settings.Value.CollectionName);
        }

        public async Task<List<Item>> GetAllAsync() =>
            await _items.Find(_ => true).ToListAsync();

        public async Task<Item> CreateAsync(Item item)
        {
            await _items.InsertOneAsync(item);
            return item;
        }
    }
}
