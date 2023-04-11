using tetris_backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace tetris_backend.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IOptions<TetrisProjectDatabaseSettings> tetrisProjectDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                tetrisProjectDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                tetrisProjectDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(
                tetrisProjectDatabaseSettings.Value.UserCollectionName);
        }

        public async Task<List<User>> GetAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string id) =>
            await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<User?> GetBasedOnEmailAsync(string email) =>
            await _userCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) =>
            await _userCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string id, User updatedUser) =>
            await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _userCollection.DeleteOneAsync(x => x.Id == id);
    }
}
