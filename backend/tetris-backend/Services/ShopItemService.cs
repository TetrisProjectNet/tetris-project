using Microsoft.Extensions.Options;
using MongoDB.Driver;
using tetris_backend.Models;

namespace tetris_backend.Services
{
    public class ShopItemService
    {
        private readonly IMongoCollection<ShopItem> _shopItemCollection;

        public ShopItemService(IOptions<TetrisProjectDatabaseSettings> tetrisProjectDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                tetrisProjectDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                tetrisProjectDatabaseSettings.Value.DatabaseName);

            _shopItemCollection = mongoDatabase.GetCollection<ShopItem>(
                tetrisProjectDatabaseSettings.Value.ShopItemCollectionName);
        }

        public async Task<List<ShopItem>> GetAsync() =>
            await _shopItemCollection.Find(_ => true).ToListAsync();

        public async Task<ShopItem?> GetAsync(string id) =>
            await _shopItemCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ShopItem newShopItem) =>
            await _shopItemCollection.InsertOneAsync(newShopItem);

        public async Task UpdateAsync(string id, ShopItem updatedShopItem) =>
            await _shopItemCollection.ReplaceOneAsync(x => x.Id == id, updatedShopItem);

        public async Task RemoveAsync(string id) =>
            await _shopItemCollection.DeleteOneAsync(x => x.Id == id);

    }
}
