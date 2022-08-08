using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBAPI.Models;

namespace MongoDBAPI.Repositories
{
    public class SocialMediaCollection : ISocialMediaCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<SocialMedia> Collection;

        public SocialMediaCollection()
        {
            Collection = _repository.db.GetCollection<SocialMedia>("SocialMedia");
        }
        public async Task DeleteSocialMedia(string code)
        {
            var filter = Builders<SocialMedia>.Filter.Eq(s => s.code, code);
            await Collection.DeleteOneAsync(filter);
        }

        public async Task<List<SocialMedia>> GetAllSocialMedia()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<SocialMedia> GetSocialMediaById(string code)
        {
            return await Collection.FindAsync(new BsonDocument { { "code", code } }).Result.FirstAsync();
        }

        public async Task InsertSocialMedia(SocialMedia socialMedia)
        {
            await Collection.InsertOneAsync(socialMedia);            
        }

        public async Task UpdateSocialMedia(SocialMedia socialMedia)
        {
            var filter = Builders<SocialMedia>.Filter.Eq(s => s.code, socialMedia.code);
            await Collection.ReplaceOneAsync(filter, socialMedia);
        }
    }
}
