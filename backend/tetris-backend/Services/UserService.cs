using tetris_backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Claims;

namespace tetris_backend.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IOptions<TetrisProjectDatabaseSettings> tetrisProjectDatabaseSettings, IHttpContextAccessor httpContextAccessor)
        {
            var mongoClient = new MongoClient(
                tetrisProjectDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                tetrisProjectDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(
                tetrisProjectDatabaseSettings.Value.UserCollectionName);

            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<User>> GetAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string id) =>
            await _userCollection.Find(x => x.id == id).FirstOrDefaultAsync();

        public async Task<User?> GetBasedOnEmailAsync(string email) =>
            await _userCollection.Find(x => x.email == email).FirstOrDefaultAsync();

        public async Task<User?> GetBasedOnUsernameAsync(string username) =>
            await _userCollection.Find(x => x.username == username).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) =>
            await _userCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string id, User updatedUser) =>
            await _userCollection.ReplaceOneAsync(x => x.id == id, updatedUser);

        public async Task UpdatePasswordAsync(string id, byte[] passwordHash, byte[] passwordSalt) =>
            await _userCollection.UpdateOneAsync(x => x.id == id, Builders<User>.Update
                .Set(u => u.passwordHash, passwordHash).Set(u => u.passwordSalt, passwordSalt));

        public async Task RemoveAsync(string id) =>
            await _userCollection.DeleteOneAsync(x => x.id == id);

        public string GetLoggedUser()
        {
            var id = string.Empty;
            var name = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                id = _httpContextAccessor.HttpContext.User.FindFirstValue("id");
                name = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return id;
        }
    }
}
