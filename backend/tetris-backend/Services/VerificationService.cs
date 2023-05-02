using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Xml;
using tetris_backend.Models;

namespace tetris_backend.Services
{
    public class VerificationService
    {
        private readonly IMongoCollection<Verification> _verificationCollection;

        public VerificationService(IOptions<TetrisProjectDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _verificationCollection = mongoDatabase.GetCollection<Verification>(
                bookStoreDatabaseSettings.Value.VerificationCollectionName);
        }

        public async Task<List<Verification>> GetAsync() =>
            await _verificationCollection.Find(_ => true).ToListAsync();

        public async Task<Verification?> GetAsync(string id) =>
            await _verificationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Verification?> GetBasedOnEmailAsync(string email) =>
            await _verificationCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

        public async Task CreateAsync(Verification newVerification)
        {
            var indexModel = new CreateIndexModel<Verification>(
                keys: Builders<Verification>.IndexKeys.Ascending("ExpireAt"),
                options: new CreateIndexOptions
                {
                    ExpireAfter = TimeSpan.FromSeconds(0),
                    Name = "ExpireAtIndex"
                }
            );
            _verificationCollection.Indexes.CreateOne(indexModel);
            await _verificationCollection.InsertOneAsync(newVerification);
        }

        public async Task UpdateAsync(string id, Verification updatedVerification) =>
            await _verificationCollection.ReplaceOneAsync(x => x.Id == id, updatedVerification);

        public async Task RemoveAsync(string id) =>
            await _verificationCollection.DeleteOneAsync(x => x.Id == id);

        public async Task RemoveBasedOnEmailAsync(string email) =>
            await _verificationCollection.DeleteOneAsync(x => x.Email == email);
    }
}
